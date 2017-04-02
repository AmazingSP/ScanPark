using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Zeig das Benutzerprofil
        /// </summary>
        /// <returns>Gibt keinen Wert zurück, sondern leitet den Benutzer zu seinem Benutzerprofil weiter</returns>
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


        /// <summary>
        /// Erstellt einen neuen Parkvorgang und eine zugehörige Rechnung.
        /// </summary>
        /// <param name="carParkId">Parkhaus-ID</param>
        /// <param name="licencePlateId">interne Kennzeichennummer</param>
        /// <param name="date">Datum</param>
        /// <param name="entrance">Einfahrt</param>
        /// <param name="exit">Ausfahrt</param>
        /// <returns>string: success</returns>
        [HttpPost]
        [ActionName("Occurence")]
        public async Task<IActionResult> CreateOccurence(int carParkId, string licencePlateId, string date, string entrance, string exit)
        {
            // Parkdauer errechnen
            DateTime en = DateTime.Parse(entrance);
            DateTime ex = DateTime.Parse(exit);
            TimeSpan ts = ex - en;


            // Neuen Parkvorgang erstellen
            OccurrenceModel occurence = new OccurrenceModel();
            occurence.LicencePlateId = licencePlateId;
            occurence.Date = date;
            occurence.Entrance = entrance;
            occurence.Exit = exit;
            occurence.Duration = ts.ToString();
            _context.Add(occurence);
            await _context.SaveChangesAsync();


            // Neue Rechnung erstellen
            BillModel bill = new BillModel();
            bill.Amount = (1.50 * ts.TotalHours).ToString();
            bill.CarParkId = carParkId;
            bill.Paied = false;
            bill.OccurenceId = occurence.OccurrenceId;

            _context.Add(bill);
            await _context.SaveChangesAsync();


            return Content("success");
        }
    }
}
