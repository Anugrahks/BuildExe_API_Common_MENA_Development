using Microsoft.AspNetCore.Mvc;

namespace BuildExeBasic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
