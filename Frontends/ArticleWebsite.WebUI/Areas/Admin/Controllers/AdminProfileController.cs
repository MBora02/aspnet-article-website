using ArticleWebsite.Dto.AppUserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ArticleWebsite.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminProfileController(IHttpClientFactory httpClientFactory)
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
                return RedirectToAction("Index", "AdminAppUser", new { area = "admin" });
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/AppUsers/profile?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                // Gelen JSON'u görmek için ekle:
                System.Diagnostics.Debug.WriteLine(json);

                var userDto = JsonConvert.DeserializeObject<GetAppUserWithAllDto>(json);

                if (userDto == null)
                {
                    TempData["Error"] = "Profil bilgileri alınamadı.";
                    return RedirectToAction("Index", "Default", new { area = "" });
                }

                return View(userDto);
            }

            TempData["Error"] = "Profil bilgileri alınamadı.";
            return RedirectToAction("Index", "Default", new { area = "" });
        }
    }
}
