using ArticleWebsite.Dto.AppUserDtos;
using ArticleWebsite.Dto.ArticleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticleWebsite.WebUI.ViewComponents.ArticleViewComponents
{
    public class _ArticleDetailAuthorByArticleComponentView:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ArticleDetailAuthorByArticleComponentView(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.blogid = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7031/api/Articles/GetArticleWithAuthorId?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetAuthorByArticleIdDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
