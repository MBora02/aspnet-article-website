using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebUI.Controllers
{
    public class ProfileUILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
