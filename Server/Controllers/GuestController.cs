using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
