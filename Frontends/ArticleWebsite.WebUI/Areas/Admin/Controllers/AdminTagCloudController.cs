using ArticleWebsite.Dto.ArticleDtos;
using ArticleWebsite.Dto.TagCloudDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ArticleWebsite.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminTagCloud")]
    public class AdminTagCloudController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminTagCloudController(IHttpClientFactory httpClientFactory)
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

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync("https://localhost:7031/api/TagClouds");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTagCloudDto>>(jsonData);
                return View(values);
            }
            TempData["Error"] = "TagCloud verileri alınamadı.";
            return View(new List<ResultTagCloudDto>());
        }

        [HttpGet]
        [Route("CreateTagCloud")]
        public IActionResult CreateTagCloud()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateTagCloud")]
        public async Task<IActionResult> CreateTagCloud(CreateTagCloudDto createTagCloudDto)
        {
            if (!ModelState.IsValid)
                return View(createTagCloudDto);

            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(createTagCloudDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7031/api/TagClouds", stringContent);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Tag oluşturulamadı. Lütfen tekrar deneyiniz.");
            return View(createTagCloudDto);
        }

        [Route("RemoveTagCloud/{id}")]
        public async Task<IActionResult> RemoveTagCloud(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"https://localhost:7031/api/TagClouds?id={id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Tag silinemedi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateTagCloud/{id}")]
        public async Task<IActionResult> UpdateTagCloud(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/TagClouds/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tagCloud = JsonConvert.DeserializeObject<UpdateTagCloudDto>(jsonData);
                return View(tagCloud);
            }
            TempData["Error"] = "Tag verisi alınamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("UpdateTagCloud/{id}")]
        public async Task<IActionResult> UpdateTagCloud(UpdateTagCloudDto updateTagCloudDto)
        {
            if (!ModelState.IsValid)
                return View(updateTagCloudDto);

            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(updateTagCloudDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7031/api/TagClouds/", stringContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Tag güncellenemedi. Lütfen tekrar deneyiniz.");
            return View(updateTagCloudDto);
        }

        [Route("UserArticles/{id}")]
        public async Task<IActionResult> UserArticles(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/GetArticleByTagCloudId?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert.DeserializeObject<List<ResultArticleWithAllDto>>(jsonData);
                ViewBag.TagCloudId = id;
                return View(articles);
            }

            TempData["Error"] = "İlgili makaleler alınamadı.";
            return View(new List<ResultArticleWithAllDto>());
        }
    }
}
