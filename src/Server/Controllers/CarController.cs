using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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


        /// <summary>
        /// Gibt eine Übersicht über alle registrierten Fahrzeuge zurück. (Nur für registrierte Benutzer)
        /// </summary>
        /// <returns>JSON-String mit vollständigem Kennzeichen, Fahrzeughersteller, Fahrzeugmodell und der internen Kennzeichennummer</returns>
        [HttpGet]
        [ActionName("Car")]
        public async Task<JsonResult> GetCars()
        {
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);

            var cars = await (from car in _context.RegisteredCars
                              join plate in _context.LicencePlates on car.RegisteredLicenceId equals plate.LicencePlateId
                              where car.RegisteredUserId == currentUser.Id
                              select new
                              {
                                  district = plate.District,
                                  identifier = plate.Identifier,
                                  number = plate.Number,
                                  brand = car.Brand,
                                  model = car.Model,
                                  plateId = plate.LicencePlateId
                              }
                             ).ToListAsync();

            return Json(cars);
        }


        /// <summary>
        /// Methode zum hinzufügen eines neues Fahrzeugs zum Profil (nur für registrierte Benutzer)
        /// </summary>
        /// <param name="district">Landkreis</param>
        /// <param name="identifier">Kennzeichen (Buchstaben)</param>
        /// <param name="number">Kennzeichen (Nummer)</param>
        /// <param name="brand">Fahrzeughersteller</param>
        /// <param name="model">Fahrzeugmodell</param>
        /// <returns>JSON-String mit success, falls das Fahrzeug angelegt wurde bzw. "error" wenn das Kennzeichen bereits registriert ist</returns>
        [HttpPost]
        [ActionName("Car")]
        public async Task<IActionResult> AddCar(string district, string identifier, string number, string brand, string model)
        {
            // Aktuellen Benutzer ermitteln
            RegisteredUserModel currentUser = await _userManager.GetUserAsync(User);


            // Prüfen, ob das Kennzeichen bereits vorhanden ist
            var licencePlate = from plate in _context.LicencePlates
                               where plate.District == district
                               where plate.Identifier == identifier
                               where plate.Number == Convert.ToInt32(number)
                               select plate;

            if (licencePlate.Count() > 0)
            {
                return Json(new { response = "error", message = "Kennzeichen existiert bereits!" });
            }


            // Nummernschild erzeugen
            LicencePlateModel newPlate = new LicencePlateModel();
            newPlate.District = district;
            newPlate.Identifier = identifier;
            newPlate.Number = Convert.ToInt32(number);

            // hinzufügen zur Datenbank
            _context.Add(newPlate);
            //await _context.SaveChangesAsync();



            // neues Fahrzeug anlegen
            RegisteredCarModel registeredCar = new RegisteredCarModel();
            registeredCar.Brand = brand;
            registeredCar.Model = model;
            registeredCar.RegisteredUserId = currentUser.Id;
            registeredCar.RegisteredLicenceId = newPlate.LicencePlateId;

            // Zur Datenbank hinzufügen
            _context.Add(registeredCar);
            await _context.SaveChangesAsync();

            return Json(new { response = "success", message = "erfolgreich angelegt" });
        }


        /// <summary>
        /// Methode zum Bearbeiten eines bestehenden Fahrzeugs bzw. Kennzeichen (nur für registrierte Benutzer)
        /// </summary>
        /// <param name="district">Landkreis</param>
        /// <param name="identifier">Kennzeichen (Buchstaben)</param>
        /// <param name="number">Kennzeichen (Nummer)</param>
        /// <param name="brand">Fahrzeughersteller</param>
        /// <param name="model">Fahrzeugmodell</param>
        /// <param name="plateId"></param>
        /// <returns>JSON-String "success"</returns>
        [HttpPut]
        [ActionName("Car")]
        public async Task<IActionResult> EditCar(string district, string identifier, string number, string brand, string model, string plateId)
        {
            LicencePlateModel plate = await _context.LicencePlates.Where(p => p.LicencePlateId.Equals(plateId, StringComparison.OrdinalIgnoreCase)).SingleAsync();
            plate.District = district;
            plate.Identifier = identifier;
            plate.Number = Convert.ToInt32(number);


            RegisteredCarModel car = await _context.RegisteredCars.Where(c => c.RegisteredLicenceId == plate.LicencePlateId).SingleAsync();
            car.Brand = brand;
            car.Model = model;

            _context.Entry(plate).State = EntityState.Modified;
            _context.Entry(car).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Content("success");
        }


        /// <summary>
        /// Methode zum Löschen eines Fahrzeugs bzw. Kennzeichen (nur für registrierte Benutzer)
        /// </summary>
        /// <param name="id">interne Kennzeichennummer. Wird über die Methode GetPlateId bzw. [HttpGet] /Car/CarId?district=<string>&identifiery=<string>&number=<int>abgerufen(</param>
        /// <returns>string: "success" wenn das Fahrzeug glöscht wurde bzw. "unpaid" falls für das zu Löschende Fahrzeug noch offene Rechnungen bestehen</returns>
        [HttpDelete]
        [ActionName("Car")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            LicencePlateModel plate = _context.LicencePlates.Where(p => p.LicencePlateId.Equals(id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            RegisteredCarModel car = _context.RegisteredCars.Where(x => x.RegisteredLicenceId == plate.LicencePlateId).SingleOrDefault();

            var result = (from occurence in _context.Occurrences
                          join bill in _context.Bills on occurence.OccurrenceId equals bill.OccurenceId
                          where occurence.LicencePlateId == car.RegisteredLicenceId
                          where bill.Paied == false
                          select occurence);

            if (result.Count() > 0)
            {
                return Content("unpaid");
            }
            else
            {
                _context.Remove(car);
                _context.Remove(plate);
                await _context.SaveChangesAsync();

                return Content("success");
            }
        }


        /// <summary>
        /// Gibt die interne Kennzeichennummer zurück. Wenn das gesuchte Kennzeichen noch nicht in der Datenbank existiert, so wird
        /// ein neues Kennzeichen angelegt und dessen interne Kennzeichennummer zurückgegeben
        /// </summary>
        /// <param name="district">Landkreis</param>
        /// <param name="identifier">Kennzeichen (Buchstaben)</param>
        /// <param name="number">Kennzeichen (Nummer)</param>
        /// <returns>string: interne Kennzeichennummer</returns>
        [HttpGet]
        [ActionName("CarId")]
        public async Task<IActionResult> GetPlateId(string district, string identifier, int number)
        {
            LicencePlateModel plateId = await (from plate in _context.LicencePlates
                                               where plate.District.Equals(district, StringComparison.OrdinalIgnoreCase)
                                               where plate.Identifier.Equals(identifier, StringComparison.OrdinalIgnoreCase)
                                               where plate.Number == number
                                               select plate).SingleOrDefaultAsync();


            //Prüfen, ob das gesuchte Kennzeichen in der Datenbank existiert
            if (plateId == null)
            {
                //   des gesuchte Kennzeichen existiert nicht nicht in der Datenbank
                // --> Kennzeichen = Gast-Kennzeichen

                LicencePlateModel newPlateId = new LicencePlateModel()
                {
                    District = district,
                    Identifier = identifier,
                    Number = number
                };

                // Abspeichern in der Datenbank
                _context.Add(newPlateId);
                await _context.SaveChangesAsync();

                return Content(newPlateId.LicencePlateId);
            }
            else
            {
                // das gesuchte Kennzeichen exisitiert in der Datenbank
                // Kennzeichen - ID übergeben
                return Content(plateId.LicencePlateId);
            }
        }
    }
}