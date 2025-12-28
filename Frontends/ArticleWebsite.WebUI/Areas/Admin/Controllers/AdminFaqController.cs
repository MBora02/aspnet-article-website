using ArticleWebsite.Dto.ArticleDtos;
using ArticleWebsite.Dto.FaqDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ArticleWebsite.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminFaq")]
    public class AdminFaqController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminFaqController(IHttpClientFactory httpClientFactory)
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
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync("https://localhost:7031/api/Faqs");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFaqDto>>(jsonData);
                return View(values);
            }
            TempData["Error"] = "Sık sorulan sorular getirilemedi.";
            return View(new List<ResultFaqDto>());
        }

        [HttpGet]
        [Route("CreateFaq")]
        public IActionResult CreateFaq()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateFaq")]
        public async Task<IActionResult> CreateFaq(CreateFaqDto createFaqDto)
        {
            if (!ModelState.IsValid) return View(createFaqDto);

            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(createFaqDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7031/api/Faqs", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Soru oluşturulamadı.");
            return View(createFaqDto);
        }

        [Route("RemoveFaq/{id}")]
        public async Task<IActionResult> RemoveFaq(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"https://localhost:7031/api/Faqs?id={id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = $"Silme başarısız: {response.StatusCode}";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateFaq/{id}")]
        public async Task<IActionResult> UpdateFaq(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Faqs/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Soru bilgisi alınamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var Faq = JsonConvert.DeserializeObject<UpdateFaqDto>(jsonData);
            return View(Faq);
        }

        [HttpPost]
        [Route("UpdateFaq/{id}")]
        public async Task<IActionResult> UpdateFaq(UpdateFaqDto updateFaqDto)
        {
            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(updateFaqDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7031/api/Faqs", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Soru güncellenemedi.");
            return View(updateFaqDto);
        }

    }
}