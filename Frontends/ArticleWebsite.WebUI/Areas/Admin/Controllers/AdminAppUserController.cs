using ArticleWebsite.Dto.AppRoleDtos;
using ArticleWebsite.Dto.AppUserDtos;
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
    [Route("Admin/AdminAppUser")]
    public class AdminAppUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminAppUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Yardımcı method: Token'lı HttpClient oluşturur
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
            var responseMessage = await client.GetAsync("https://localhost:7031/api/AppUsers/with-all");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetAppUserWithAllDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateAppUser")]
        public async Task<IActionResult> CreateAppUserAsync()
        {
            var client = CreateAuthorizedClient();

            var roleResponse = await client.GetAsync("https://localhost:7031/api/AppRole");
            var depResponse = await client.GetAsync("https://localhost:7031/api/Departments");

            ViewBag.AppRoles = roleResponse.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<ResultAppRoleDto>>(await roleResponse.Content.ReadAsStringAsync())
                : new List<ResultAppRoleDto>();

            ViewBag.Departments = depResponse.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<ResultDepartmentDto>>(await depResponse.Content.ReadAsStringAsync())
                : new List<ResultDepartmentDto>();

            return View();
        }

        [HttpPost]
        [Route("CreateAppUser")]
        public async Task<IActionResult> CreateAppUser(CreateAppUserDto createAppUserDto)
        {
            if (!ModelState.IsValid)
                return View(createAppUserDto);

            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(createAppUserDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7031/api/AppUsers", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminAppUser", new { area = "Admin" });
            }

            ModelState.AddModelError("", "Failed to create AppUser. Please try again.");
            return View(createAppUserDto);
        }

        [Route("RemoveAppUser/{id}")]
        public async Task<IActionResult> RemoveAppUser(int id)
        {
            var client = CreateAuthorizedClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7031/api/AppUsers?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminAppUser", new { area = "Admin" });
            }

            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            TempData["DeleteError"] = $"Kullanıcı silinemedi. Hata: {responseMessage.StatusCode} - {errorContent}";
            return RedirectToAction("Index", "AdminAppUser", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateAppUser/{id}")]
        public async Task<IActionResult> UpdateAppUser(int id)
        {
            var client = CreateAuthorizedClient();

            // Kullanıcı verisini çek
            var userResponse = await client.GetAsync($"https://localhost:7031/api/AppUsers/{id}");
            if (!userResponse.IsSuccessStatusCode) return View();

            var userJson = await userResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UpdateAppUserDto>(userJson);

            // Roller
            var roleResponse = await client.GetAsync("https://localhost:7031/api/AppRole");
            ViewBag.AppRoles = roleResponse.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<ResultAppRoleDto>>(await roleResponse.Content.ReadAsStringAsync())
                    .Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = r.AppRoleName,
                        Value = r.AppRoleId.ToString(),
                        Selected = r.AppRoleId == user.AppRoleId
                    }).ToList()
                : new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            // Departmanlar
            var depResponse = await client.GetAsync("https://localhost:7031/api/Departments");
            ViewBag.Departments = depResponse.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<ResultDepartmentDto>>(await depResponse.Content.ReadAsStringAsync())
                    .Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = d.Name,
                        Value = d.DepartmentId.ToString(),
                        Selected = d.DepartmentId == user.DepartmentId
                    }).ToList()
                : new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            return View(user);
        }

        [HttpPost]
        [Route("UpdateAppUser/{id}")]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserDto updateAppUserDto)
        {
            var client = CreateAuthorizedClient();
            var jsonData = JsonConvert.SerializeObject(updateAppUserDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7031/api/AppUsers/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminAppUser", new { area = "Admin" });
            }

            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Güncelleme başarısız. Hata: {responseMessage.StatusCode} - {errorContent}");
            return View(updateAppUserDto);
        }

        [Route("UserArticles/{id}")]
        public async Task<IActionResult> UserArticles(int id)
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"https://localhost:7031/api/Articles/GetArticleByAuthorId?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert.DeserializeObject<List<ResultArticleWithAllDto>>(jsonData);
                ViewBag.UserId = id;
                return View(articles);
            }

            return View(new List<ResultArticleWithAllDto>());
        }
    }
}
