using System.Net.Http.Headers;
using System.Security.Claims;
using ArticleWebsite.Dto.AppUserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticleWebsite.WebUI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient CreateAuthorizedClient()
        {
            var token = User.FindFirst("articlewebsitetoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }
        public async Task<IActionResult> Index()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "Kullanıcı kimliği bulunamadı.";
                return RedirectToAction("Login", "Account"); // Veya uygun login sayfası
            }

            var client = CreateAuthorizedClient();

            // API endpoint'in route parametre olarak id bekliyorsa:
            var response = await client.GetAsync($"https://localhost:7031/api/AppUsers/profile?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var userDto = JsonConvert.DeserializeObject<GetAppUserWithAllDto>(json);

                if (userDto == null)
                {
                    TempData["Error"] = "Profil bilgileri alınamadı.";
                    return RedirectToAction("Index", "Home");
                }

                return View(userDto);
            }
            TempData["Error"] = "Profil bilgileri alınamadı.";
            return RedirectToAction("Index", "Home");
        }
    }
}
