using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
