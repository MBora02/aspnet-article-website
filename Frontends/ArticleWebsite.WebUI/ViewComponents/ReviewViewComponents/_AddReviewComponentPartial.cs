using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.ViewComponents.ReviewViewComponents
{
    public class _AddReviewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
