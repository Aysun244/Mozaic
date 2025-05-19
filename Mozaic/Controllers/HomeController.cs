using Microsoft.AspNetCore.Mvc;

namespace Mozaic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
