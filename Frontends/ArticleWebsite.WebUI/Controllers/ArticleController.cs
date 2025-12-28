using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ArticleWebsite.Dto.ArticleDtos;
using ArticleWebsite.Dto.ReviewDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticleWebsite.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ArticleController(IHttpClientFactory httpClientFactory)
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
            ViewBag.v1 = "Makaleler";
            ViewBag.v2 = "Yazarlarımızın Makaleleri";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7031/api/Articles/with-all");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultArticleWithAllDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultArticleWithAllDto>());
        }

        public async Task<IActionResult> ArticleDetail(int id)
        {
            ViewBag.v1 = "Makaleler";
            ViewBag.v2 = "Makale Detayı ve Yorumlar";
            ViewBag.articleId = id;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Makale bilgisi alınamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var article = JsonConvert.DeserializeObject<ResultArticleWithAllDto>(jsonData);

            return View(article);
        }

        [HttpGet]
        public PartialViewResult AddReview(int id)
        {
            ViewBag.articleId = id;
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto createReviewDto)
        {
            // Token'dan kullanıcı bilgilerini çek
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out var reviewerId))
            {
                createReviewDto.ReviewerId = reviewerId;
            }

            createReviewDto.ReviewerName = User.FindFirst("name")?.Value ?? "Anonim";
            createReviewDto.ReviewerSurname = User.FindFirst("surname")?.Value ?? "";
            createReviewDto.ReviewerEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            createReviewDto.CreatedAt = DateTime.UtcNow;

            var client = CreateAuthorizedClient();

            var json = JsonConvert.SerializeObject(createReviewDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7031/api/Reviews", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ArticleDetail", new { id = createReviewDto.ArticleId });
            }

            TempData["Error"] = "Yorum eklenemedi.";
            return RedirectToAction("ArticleDetail", new { id = createReviewDto.ArticleId });
        }

    }
}
