using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _HeadUILayoutViewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
