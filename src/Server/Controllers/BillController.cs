using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Identity;
using System.Linq;
using System.Threading.Tasks;


namespace Server.Controllers
{
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<RegisteredUserModel> _userManager;


        public BillController(ApplicationDbContext context, UserManager<RegisteredUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        [ActionName("Bill")]
        public async Task<JsonResult> GetBills()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);

            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.ProfileId == currentUser.ProfileId);

            var bill3 = await (from x in _context.Bills
                               where x.BillId == 2
                               select x).ToListAsync();

            return Json(bill3);
        }


        [HttpPost]
        [ActionName("Bill")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBill([Bind("BillId,LicensePlate,Date,Entrence,Exit,Duration,Amount,Paied")] BillModel bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bill);
        }


        [HttpPut]
        [ActionName("Bill")]
        public async Task<JsonResult> PayBill(int id)
        {
            BillModel result = _context.Bills.Where(x => x.BillId == id).SingleOrDefault();
            result.Paied = true;

            _context.Entry(result).State = EntityState.Modified;
            _context.SaveChanges();


            await Task.Delay(3000);
            return Json(new { status = "success" });
        }
    }
}
