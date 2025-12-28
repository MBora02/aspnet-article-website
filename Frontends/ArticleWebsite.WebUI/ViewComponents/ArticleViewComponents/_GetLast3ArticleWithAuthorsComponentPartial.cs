using ArticleWebsite.Dto.ArticleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticleWebsite.WebUI.ViewComponents.ArticleViewComponents
{
    public class _GetLast3ArticleWithAuthorsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _GetLast3ArticleWithAuthorsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7031/api/Articles/GetLast3ArticlesWitAuthorsList");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLast3ArticlesWithAuthorsDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultLast3ArticlesWithAuthorsDto>());
        }
    }
}
