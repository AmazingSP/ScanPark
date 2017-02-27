using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<UserIdentity> userManager;
        
        public UserController(UserManager<UserIdentity> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Bills");
        }

        public IActionResult Bills()
        {
            UserIdentity user = userManager.GetUserAsync(HttpContext.User).Result;
            ViewBag.FullName = string.Concat(user.FirstName, " ", user.LastName);
            ViewBag.EMail = user.Email;
            return View();
        }

        public IActionResult Cars()
        {
            UserIdentity user = userManager.GetUserAsync(HttpContext.User).Result;
            ViewBag.FullName = string.Concat(user.FirstName, " ", user.LastName);
            ViewBag.EMail = user.Email;
            return View();
        }
    }
}