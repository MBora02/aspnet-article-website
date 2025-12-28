using System.Net.Http.Headers;
using System.Text;
using ArticleWebsite.Dto.ArticleDtos;
using ArticleWebsite.Dto.DepartmentDtos;
using ArticleWebsite.Dto.TagCloudDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticleWebsite.WebUI.Controllers
{
    [Authorize]
    public class UserArticleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserArticleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient CreateAuthorizedClient()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "articlewebsitetoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        public async Task<IActionResult> Index()
        {
            var client = CreateAuthorizedClient();

            // Kullanıcının ID'sini almak için ClaimTypes.NameIdentifier kullanılabilir
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // API tarafında kullanıcıya göre makaleler çekmek için endpoint varsa onu kullan
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/GetArticleByAuthorId?id={userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert.DeserializeObject<List<ResultArticleWithAllDto>>(jsonData);
                return View(articles);
            }
            TempData["Error"] = "Makale getirilemedi.";
            return View(new List<ResultArticleWithAllDto>());
        }

        [HttpPost]
        public async Task<IActionResult> RemoveArticle(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"https://localhost:7031/api/Articles?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Makale silinemedi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateArticle()
        {
            await LoadSelectLists();
            return View(); // Boş formu gösterir
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleDto model)
        {
            
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var userName = User.FindFirst("name")?.Value;
            var userSurname = User.FindFirst("surname")?.Value;
            if (!int.TryParse(userId, out var authorId))
            {
                TempData["Error"] = "Kullanıcı ID alınamadı veya geçersiz.";
                return RedirectToAction("Index");
            }
            // Yazar bilgilerini model'e ekle
            model.AuthorId = authorId;
            model.AuthorEmail = userEmail;
            model.AuthorName = userName;
            model.AuthorSurname = userSurname;
            // Oluşturulma tarihi ekle
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.Content), "Content");
            content.Add(new StringContent(model.AuthorId.ToString()), "AuthorId");
            content.Add(new StringContent(model.AuthorEmail ?? ""), "AuthorEmail");
            content.Add(new StringContent(model.AuthorName ?? ""), "AuthorName");
            content.Add(new StringContent(model.AuthorSurname ?? ""), "AuthorSurname");
            content.Add(new StringContent(model.DepartmentId.ToString()), "DepartmentId");
            content.Add(new StringContent(model.TagCloudId.ToString()), "TagCloudId");
            content.Add(new StringContent("1"), "StatusId");
            if (model.PdfFile != null)
                content.Add(new StreamContent(model.PdfFile.OpenReadStream()), "PdfFile", model.PdfFile.FileName);

            if (model.ImageFile != null)
                content.Add(new StreamContent(model.ImageFile.OpenReadStream()), "ImageFile", model.ImageFile.FileName);

            var client = CreateAuthorizedClient();
            var response = await client.PostAsync("https://localhost:7031/api/Articles", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Makale başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Makale oluşturulamadı.";
            await LoadSelectLists();
            return View(model);
        }

            [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Makale bulunamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var article = JsonConvert.DeserializeObject<UpdateArticleDto>(jsonData); // uygun Dto

            return View(article);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateArticle(UpdateArticleDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out var authorId) || authorId != model.AuthorId)
            {
                TempData["Error"] = "Yetkisiz işlem.";
                return RedirectToAction("Index");
            }

            var client = CreateAuthorizedClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7031/api/Articles/{model.ArticleId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Makale güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Makale güncellenemedi.";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendForApproval(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out var authorId))
            {
                TempData["Error"] = "Kullanıcı ID alınamadı veya geçersiz.";
                return RedirectToAction("Index");
            }

            var client = CreateAuthorizedClient();

            // API'de PUT metodu olduğu için PutAsync kullanıyoruz, body null
            var response = await client.PutAsync($"https://localhost:7031/api/Articles/SendForApproval/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Makale onaya gönderildi.";
            }
            else
            {
                TempData["Error"] = "Makale onaya gönderilemedi.";
            }

            return RedirectToAction("Index");
        }
        private async Task LoadSelectLists()
        {
            var client = CreateAuthorizedClient();

            var deptResponse = await client.GetAsync("https://localhost:7031/api/Departments");
            var tagResponse = await client.GetAsync("https://localhost:7031/api/TagClouds");

            if (deptResponse.IsSuccessStatusCode)
            {
                var deptJson = await deptResponse.Content.ReadAsStringAsync();
                ViewBag.Departments = JsonConvert.DeserializeObject<List<ResultDepartmentDto>>(deptJson);
            }
            else
            {
                ViewBag.Departments = new List<ResultDepartmentDto>();
            }

            if (tagResponse.IsSuccessStatusCode)
            {
                var tagJson = await tagResponse.Content.ReadAsStringAsync();
                ViewBag.TagClouds = JsonConvert.DeserializeObject<List<ResultTagCloudDto>>(tagJson);
            }
            else
            {
                ViewBag.TagClouds = new List<ResultTagCloudDto>();
            }
        }

    }
}
