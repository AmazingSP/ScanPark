using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Models;
using Microsoft.AspNetCore.Identity;

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
            UserIdentity user = userManager.GetUserAsync(HttpContext.User).Result;

            ViewBag.Message = $"Welcome {user.FirstName}!";
            if (userManager.IsInRoleAsync(user, "NormalUser").Result)
            {
                ViewBag.RoleMessage = "You are a NormalUser.";
            }
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
    }
}