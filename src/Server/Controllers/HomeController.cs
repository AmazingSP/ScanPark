﻿using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Details", "Profile");
                }
            }
            return RedirectToAction("Index", "Account");
        }
    }
}
