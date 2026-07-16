using ArticleWebsite.Dto.ArticleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ArticleWebsite.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminArticle")]
    public class AdminArticleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminArticleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // JWT tokenlı HttpClient oluştur
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
            var response = await client.GetAsync("https://localhost:7031/api/Articles/with-all");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert.DeserializeObject<List<ResultArticleWithAllDto>>(jsonData);
                return View(articles);
            }
            TempData["Error"] = "Makaleler getirilemedi.";
            return View(new List<ResultArticleWithAllDto>());
        }

        [Route("RemoveArticle/{id}")]
        public async Task<IActionResult> RemoveArticle(int id)
        {
            var client = CreateAuthorizedClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7031/api/Articles?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = $"Makale silinemedi. Hata: {responseMessage.StatusCode}";
            return RedirectToAction("Index");
        }

        [Route("ArticleDetail/{id}")]
        public async Task<IActionResult> ArticleDetail(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var article = JsonConvert.DeserializeObject<ResultArticleWithAllDto>(jsonData);
                return View(article);
            }
            TempData["Error"] = "Makale bulunamadı.";
            return RedirectToAction("Index");
        }
    }
}
