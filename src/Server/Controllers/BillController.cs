using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Globalization;
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


        /// <summary>
        /// Gibt eine Übersicht über alle Rechnungen zu dem angemeldeten Benutzer oder Gast zurück
        /// </summary>
        /// <param name="plateId">32-stellige Eindeutige Nummer (Nur für Gastbenutzer), sonst null</param>
        /// <returns>JSON-String mit allen Informationen (Parkhaus Name, vollständiges Kennzeichen, Datum, Einfahrt, Ausfahrt, Dauer, Betrag, Status (bezahlt / unbezahlt), interne Rechnungsnumer</returns>
        [HttpGet]
        [ActionName("Bill")]
        public async Task<IActionResult> GetBills(string plateId = default(string))
        {
            CultureInfo ci = new CultureInfo("de-DE");

            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);

            if(currentUser != null)
            {
                var bills = await (from bill in _context.Bills
                                   join carPark in _context.CarParks on bill.CarParkId equals carPark.CarParkId
                                   join occurence in _context.Occurrences on bill.OccurenceId equals occurence.OccurrenceId
                                   join plate in _context.LicencePlates on occurence.LicencePlateId equals plate.LicencePlateId
                                   join car in _context.RegisteredCars on plate.LicencePlateId equals car.RegisteredLicenceId
                                   where car.RegisteredUserId == currentUser.Id
                                   select new
                                   {
                                       carPark = carPark.CarkParkName,
                                       licensePlate = string.Concat(plate.District, " ", plate.Identifier, " ", plate.Number),
                                       date = occurence.Date,
                                       entrance = occurence.Entrance,
                                       exit = occurence.Exit,
                                       duration = occurence.Duration,
                                       amount = string.Format("{0:#.00} €", bill.Amount),
                                       paied = bill.Paied,
                                       billId = bill.BillId
                                   }).ToListAsync();
                return Json(bills);
            }
            else
            {
                var bills = await (from bill in _context.Bills
                                   join carPark in _context.CarParks on bill.CarParkId equals carPark.CarParkId
                                   join occurence in _context.Occurrences on bill.OccurenceId equals occurence.OccurrenceId
                                   join plate in _context.LicencePlates on occurence.LicencePlateId equals plate.LicencePlateId
                                   //join car in _context.RegisteredCars on plate.LicencePlateId equals car.RegisteredLicenceId
                                   where occurence.LicencePlateId.Equals(plateId, StringComparison.OrdinalIgnoreCase)
                                   select new
                                   {
                                       carPark = carPark.CarkParkName,
                                       licensePlate = string.Concat(plate.District, " ", plate.Identifier, " ", plate.Number),
                                       date = occurence.Date,
                                       entrance = occurence.Entrance,
                                       exit = occurence.Exit,
                                       duration = occurence.Duration,
                                       amount = string.Format("{0:#.00} €", bill.Amount),
                                       paied = bill.Paied,
                                       billId = bill.BillId
                                   }).ToListAsync();
                return Json(bills);
            }
        }


        /// <summary>
        /// Methode zum Bezahlen der Rechnung
        /// </summary>
        /// <param name="id">interne Rechnungsnummer. Wird über die Methode GetBills bzw. [HttpGet] /Bill/Bill abgerufen</param>
        /// <returns>JSTON-String mit "success"</returns>
        [HttpPut]
        [ActionName("Bill")]
        public async Task<JsonResult> PayBill(int id)
        {
            BillModel result = _context.Bills.Where(x => x.BillId == id).SingleOrDefault();
            result.Paied = true;

            _context.Entry(result).State = EntityState.Modified;
            _context.SaveChanges();


            await Task.Delay(0);
            return Json(new { status = "success" });
        }
    }
}
