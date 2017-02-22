using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Server.Controllers
{
    [AuthorizeAttribute]
    public class AdminController : Controller
    {
        private readonly DataBaseContext dbc = new DataBaseContext();
        
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            return Json(dbc.Users.ToList());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
