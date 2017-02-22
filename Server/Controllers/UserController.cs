using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bills()
        {
            return View();
        }

        public IActionResult Cars()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
