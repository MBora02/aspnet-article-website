using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.ViewComponents.ArticleViewComponents
{
    public class _ArticleDetailsTagCloudComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
