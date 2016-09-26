using Microsoft.AspNetCore.Mvc;

namespace Aparts.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}