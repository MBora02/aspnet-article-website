using System.Text;
using ArticleWebsite.Dto.DepartmentDtos;
using ArticleWebsite.Dto.RegisterDto;
using ArticleWebsite.Dto.TagCloudDtos;
using ArticleWebsite.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ArticleWebsite.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> CreateAppUser()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7031/api/Departments");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var departmentList = JsonConvert.DeserializeObject<List<ResultDepartmentDto>>(jsonData);
                ViewBag.Departments = new SelectList(departmentList, "DepartmentId", "Name");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateRegisterDto createRegisterDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createRegisterDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7031/api/Register", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }

            // Hata anında tekrar bölümleri doldurmalıyız
            var departmentResponse = await client.GetAsync("https://localhost:7031/api/Departments");
            if (departmentResponse.IsSuccessStatusCode)
            {
                var departmentJson = await departmentResponse.Content.ReadAsStringAsync();
                var departmentList = JsonConvert.DeserializeObject<List<ResultDepartmentDto>>(departmentJson);
                ViewBag.Departments = new SelectList(departmentList, "DepartmentId", "Name");
            }

            
            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ApiValidationError>>(errorContent);

            foreach (var error in errors)
            {
                // propertyName örneğin "email" ise "Email" şeklinde eşleşmesi için PascalCase yapıyoruz
                var key = char.ToUpper(error.PropertyName[0]) + error.PropertyName.Substring(1);
                ModelState.AddModelError(key, error.ErrorMessage);
            }

            return View(createRegisterDto);
        }
    }
}
