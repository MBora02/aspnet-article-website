using System.Net.Http.Headers;
using System.Text.Json;
using ArticleWebsite.Dto.ArticleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class UserInstructorArticleCheckController : Controller
    {
        
        private readonly IHttpClientFactory _httpClientFactory;

        public UserInstructorArticleCheckController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient CreateAuthorizedClient()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "articlewebsitetoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
        public async Task<IActionResult> Index()
        {
            var client = CreateAuthorizedClient();

            // Kullanıcıdan departmentId al (ör: claim'den)
            var departmentIdClaim = User.FindFirst("DepartmentId")?.Value;

            if (string.IsNullOrEmpty(departmentIdClaim) || !int.TryParse(departmentIdClaim, out int departmentId))
            {
                TempData["Error"] = "Departman bilgisi alınamadı.";
                return View(new List<ResultArticleWithAllDto>());
            }

            var response = await client.GetAsync($"https://localhost:7031/api/Articles/GetArticleByDepartmentId?id={departmentId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Makaleler alınamadı.";
                return View(new List<ResultArticleWithAllDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var articles = JsonSerializer.Deserialize<List<ResultArticleWithAllDto>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(articles);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveArticle(int id)
        {
            var client = CreateAuthorizedClient();

            var response = await client.PutAsync($"https://localhost:7031/api/Articles/Approve/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Makale onaylandı.";
            }
            else
            {
                TempData["Error"] = "Makale onaylanamadı.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> RejectArticle(int id)
        {
            var client = CreateAuthorizedClient();

            var response = await client.PutAsync($"https://localhost:7031/api/Articles/Reject/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Makale reddedildi.";
            }
            else
            {
                TempData["Error"] = "Makale reddedilemedi.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ArticleDetail(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Makale bulunamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var article = JsonSerializer.Deserialize<ResultArticleWithAllDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(article);
        }


    }
}
