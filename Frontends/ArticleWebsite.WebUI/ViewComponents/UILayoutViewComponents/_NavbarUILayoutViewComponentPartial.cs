using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutViewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
