using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
