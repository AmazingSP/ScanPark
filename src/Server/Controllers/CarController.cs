using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace Server.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<RegisteredUserModel> _userManager;


        public CarController(ApplicationDbContext context, UserManager<RegisteredUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        [ActionName("Car")]
        public async Task<JsonResult> GetCars()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);

            var cars = await(from x in _context.RegisteredPlates
                              where x.ProfileId == currentUser.ProfileId
                              select x).ToListAsync();

            return Json(cars);
        }

 
        [HttpPost]
        [ActionName("Car")]
        public async Task<IActionResult> AddCar([FromBody] RegisteredPlateModel model)
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync (User);

            RegisteredPlateModel plate = new RegisteredPlateModel();
            plate.PlateDistrict = model.PlateDistrict;
            plate.PlateIdentifier = model.PlateIdentifier;
            plate.PlateNumber = model.PlateNumber;
            plate.Brand = model.Brand;
            plate.Model = model.Model;
            plate.ProfileId = currentUser.ProfileId;

            _context.Add(plate);
            await _context.SaveChangesAsync();

            //return Json(new string[] { "success" });
            return Content("success");
        }


        [HttpPut]
        [ActionName("Car")]
        public async Task<IActionResult> EditCar([FromBody] RegisteredPlateModel model)
        {
            RegisteredPlateModel edit = _context.RegisteredPlates.Where(x => x.RegisteredPlateId == model.RegisteredPlateId).Single();

            edit.PlateDistrict = model.PlateDistrict;
            edit.PlateIdentifier = model.PlateIdentifier;
            edit.PlateNumber = model.PlateNumber;
            edit.Brand = model.Brand;
            edit.Model = model.Model;
            

            _context.Entry(edit).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("success");
        }


        [HttpDelete]
        [ActionName("Car")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            RegisteredPlateModel remove = _context.RegisteredPlates.Where(x => x.RegisteredPlateId == id).SingleOrDefault();

            _context.Remove(remove);
            await _context.SaveChangesAsync();

            return Content("success");
        }
    }
}