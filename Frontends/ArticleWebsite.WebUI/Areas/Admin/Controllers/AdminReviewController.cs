using ArticleWebsite.Dto.ReviewDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ArticleWebsite.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminReview")]
    public class AdminReviewController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminReviewController(IHttpClientFactory httpClientFactory)
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

        [Route("ArticleReviews/{id}")]
        public async Task<IActionResult> ArticleReviews(int id)
        {
            ViewBag.Id = id;
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Reviews/GetReviewByArticleId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<ResultReviewDto>>(jsonData);
                return View(reviews);
            }
            TempData["Error"] = "Yorumlar getirilemedi.";
            return View(new List<ResultReviewDto>());
        }

        [Route("RemoveReview/{id}/{articleId}")]
        public async Task<IActionResult> RemoveReview(int id, int articleId)
        {
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"https://localhost:7031/api/Reviews?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ArticleReviews", new { id = articleId });
            }
            TempData["Error"] = "Yorum silinemedi.";
            return RedirectToAction("ArticleReviews", new { id = articleId });
        }
    }
}
