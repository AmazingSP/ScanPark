using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Identity;
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


        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Details");
        }


        [HttpGet]
        public async Task<IActionResult> Details()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.ProfileId == currentUser.ProfileId);
            return View(profile);
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.ProfileId == currentUser.ProfileId);
            return View(profile);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProfileId,FirstName,LastName,Street,City,ZipCode,Country,PhoneNumber,BankName,IBAN,BIC")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("Details");
            }
            return View(profile);
        }


        [HttpGet]
        public IActionResult Bills()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Cars()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Map()
        {
            return View();
        }
    }
}
