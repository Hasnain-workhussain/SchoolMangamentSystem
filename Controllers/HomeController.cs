using Microsoft.AspNetCore.Mvc;

namespace SchoolMangamentSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}