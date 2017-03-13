using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Identity;
using Server.Models.ViewModels;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<RegisteredUserModel> _userManager;
        private SignInManager<RegisteredUserModel> _signInManagerForRegisteredUsers;
        private ApplicationDbContext _context;
        //private SignInManager<GuestUser> _signInManagerForGuests;

        public AccountController(UserManager<RegisteredUserModel> userManager, SignInManager<RegisteredUserModel> signInManagerForRegisteredusers, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManagerForRegisteredUsers = signInManagerForRegisteredusers;
            //_signInManagerForGuests = signInManagerForGuests;
            _context = context;
        }


        // View Landingpage
        public IActionResult Index()
        {
            return View();  
        }


        // Methode Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerForRegisteredUsers.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Details", "Profile");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login fehlgeschlagen");
                }
            }

            return RedirectToAction("Index");
        }


        // Methode Logoff
        public async Task<IActionResult> Logoff()
        {
            await _signInManagerForRegisteredUsers.SignOutAsync();
            return RedirectToAction("Index");
        }


        // Methode Registrierung
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisteredUserModel user = new RegisteredUserModel { UserName = model.Email, Email = model.Email, Profile = new ProfileModel() };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManagerForRegisteredUsers.SignInAsync(user, false);
                    return RedirectToAction("Details", "Profile");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registrierung fehlgeschlagen");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }


        // View Guest
        [HttpGet]
        public JsonResult Guest(GuestViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return Json(new string[] { model.LicensePlate, model.OneTimePaymentCode });
            //if (ModelState.IsValid)
            //{
            //    var result = await _signInManagerForGuests.PasswordSignInAsync(model.LicensePlate, model.OneTimePaymentCode, false, false);

            //    if (result.Succeeded)
            //    {
            //        // WEITERLEITUNG ANPASSEN !!!!
            //        return RedirectToAction("Details", "Profile");
            //        // WEITERLEITUNG ANPASSEN !!!!
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Unbekanntest Kennzeichen oder falscher One-Time Paymentcode");
            //    }
            //}

            //return View();
        }
    }
}
