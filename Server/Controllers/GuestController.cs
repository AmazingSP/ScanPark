using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    public class GuestController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OneTimePayment(GuestViewModel obj)
        {
            ViewBag.FullName = "Als Gast angemeldet";
            ViewBag.LicensePlate = obj.LicensePlate;
            return View();
        }
    }
}