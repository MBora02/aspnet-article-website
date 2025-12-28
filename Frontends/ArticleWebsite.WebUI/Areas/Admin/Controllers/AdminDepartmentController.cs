using ArticleWebsite.Dto.ArticleDtos;
using ArticleWebsite.Dto.DepartmentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ArticleWebsite.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminDepartment")]
    public class AdminDepartmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDepartmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Yardımcı method: JWT'li HTTP client oluştur
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

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync("https://localhost:7031/api/Departments");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDepartmentDto>>(jsonData);
                return View(values);
            }
            TempData["Error"] = "Departmanlar getirilemedi.";
            return View(new List<ResultDepartmentDto>());
        }

        [HttpGet]
        [Route("CreateDepartment")]
        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            if (!ModelState.IsValid) return View(createDepartmentDto);

            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(createDepartmentDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7031/api/Departments", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Departman oluşturulamadı.");
            return View(createDepartmentDto);
        }

        [Route("RemoveDepartment/{id}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"https://localhost:7031/api/Departments?id={id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = $"Silme başarısız: {response.StatusCode}";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Departments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Departman bilgisi alınamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var department = JsonConvert.DeserializeObject<UpdateDepartmentDto>(jsonData);
            return View(department);
        }

        [HttpPost]
        [Route("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(updateDepartmentDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7031/api/Departments", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Departman güncellenemedi.");
            return View(updateDepartmentDto);
        }

        [Route("UserArticles/{id}")]
        public async Task<IActionResult> UserArticles(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/GetArticleByDepartmentId?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert.DeserializeObject<List<ResultArticleWithAllDto>>(jsonData);
                ViewBag.DepartmentId = id;
                return View(articles);
            }

            ModelState.AddModelError("", "Makaleler alınamadı.");
            return View(new List<ResultArticleWithAllDto>());
        }
    }
}
