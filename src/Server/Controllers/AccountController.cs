using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.ViewModels;
using System;
using System.Linq;
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
            _context = context;
        }


        // View Landingpage
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Login Methode zur Authentifizierung von regisrierten Benutzern.
        /// </summary>
        /// <param name="model">Die Felder "E-Mail Adresse" und "Kennwort" werden über das Model übergeben</param>
        /// <returns>Gibt keinen Wert zurück sondern leitet den Benutzer nach erfolgreicher Anmeldung zur Benutzerseite weiter</returns>
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


        /// <summary>
        /// Logout Methode zum Abmelden. Sowohl für registrierte Benutzer als auch Gastbenutzer
        /// </summary>
        /// <returns>Leitet den Benutzer nach dem Logout zurück zur Landing Page (Login, Registrieren, Gast)</returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManagerForRegisteredUsers.SignOutAsync();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Registrierung Methode für neue Benutzer.
        /// </summary>
        /// <param name="model">Die Felder "E-Mail Adresse" und "Kennwort" werden über das Model übergeben</param>
        /// <returns>Gibt keinen Wert zurück sondern leitet den Benutzer zur Profilübersicht weiter</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            RegisteredUserModel user = new RegisteredUserModel { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManagerForRegisteredUsers.SignInAsync(user, false);
                return RedirectToAction("Details", "Profile");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registrierung fehlgeschlagen");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return RedirectToAction("Register", "Account");
            }
        }


        /// <summary>
        /// Gast Methode. Dient zum Einloggen mit einem Einmal-Zahlungscode
        /// </summary>
        /// <param name="model">Es werden die Felder "Landkreis", "Kennung" und "Nummer" über das Model übergeben</param>
        /// <returns>Gibt keinen Wert zurück sondern leitet den Gast zur Rechungsübersicht.</returns>
        [HttpPost]
        public async Task<IActionResult> Guest(GuestViewModel model)
        {
            LicencePlateModel plateId = await (from plate in _context.LicencePlates
                                               where plate.District.Equals(model.District, StringComparison.OrdinalIgnoreCase)
                                               where plate.Identifier.Equals(model.Identifier, StringComparison.OrdinalIgnoreCase)
                                               where plate.Number == model.Number
                                               select plate).SingleOrDefaultAsync();

            if (plateId == null)
            {
                ModelState.AddModelError(string.Empty, "Unbekanntes Kennzeichen oder falscher One-Time Paymentcode");
                return RedirectToAction("Index", "Account");
            }

            if (plateId.LicencePlateId.Equals(model.OneTimePaymentCode))
            {
                return RedirectToAction("Guest", "Profile", model);
            }

            return RedirectToAction("Index", "Account");
        }
    }
}
