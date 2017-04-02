using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.ViewModels;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<RegisteredUserModel> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<RegisteredUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Leitet den Benutzer zur Profilseite weiter
        /// </summary>
        /// <returns>Kein Rückgabewert, sondern leitet den Benutzer zur Profilseite weiter</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Details");
        }


        /// <summary>
        /// Zeigt die Profilseite an mit allen Details des Registrierten Benutzers
        /// </summary>
        /// <returns>Zeigt die Profilseite an mit allen Details des Registrierten Benutzers (RegisteredUserModel)</returns>
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.RegisteredUsers.SingleOrDefaultAsync(m => m.Id == currentUser.Id);
            return View(profile);
        }


        /// <summary>
        /// Gibt die View mit den Benutzerdaten zurück
        /// </summary>
        /// <returns>Gibt die View mit den Benutzerdaten zurück (RegisteredUserModel)</returns>
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.RegisteredUsers.SingleOrDefaultAsync(m => m.Id == currentUser.Id);
            return View(profile);
        }


        /// <summary>
        /// Methode zum Editieren des Benutzerprofils
        /// </summary>
        /// <param name="profile">Übernimmt die Formularfelder (ID,FirstName,LastName,Street,City,ZipCode,Country,BankName,IBAN,BIC) und fügt die Werte in das RegisteredUserModel</param>
        /// <returns>Leitet den Benutzer zur Benutzerprofilseite (redirect / refresh)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,FirstName,LastName,Street,City,ZipCode,Country,BankName,IBAN,BIC")] RegisteredUserModel profile)
        {           
            var userToUpdate = await _context.RegisteredUsers.SingleOrDefaultAsync(s => s.Id == profile.Id);

            if (await TryUpdateModelAsync(userToUpdate, "", s => s.BankName,
                                                                    s => s.BIC,
                                                                    s => s.City,
                                                                    s => s.Country,
                                                                    s => s.FirstName,
                                                                    s => s.IBAN,
                                                                    s => s.LastName,
                                                                    s => s.Street,
                                                                    s => s.ZipCode))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(userToUpdate);
        }


        /// <summary>
        /// Zeigt die Seite mit den Rechnungen an
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Bills()
        {
            return View();
        }


        /// <summary>
        /// Zeigt die Seite mit den Fahrzeugen an (nur bei registrierten Benutzern)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Cars()
        {
            return View();
        }


        /// <summary>
        /// Zeigt die Seite mit allen Parkhäusern an (Google Maps)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Map()
        {
            return View();
        }


        /// <summary>
        /// Zeigt die Seite für nicht registrierte Benutzer an
        /// </summary>
        /// <param name="model">GuestViewModel aus der View</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Guest(GuestViewModel model)
        {
            return View(model);
        }
    }
}
