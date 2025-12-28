using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _MainCoverUILayoutViewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
