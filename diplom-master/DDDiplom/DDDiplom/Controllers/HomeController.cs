using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDDiplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DDDiplom.Controllers
{
    public class HomeController : Controller
    {
        private DDDiplomContext db;
        public HomeController(DDDiplomContext context)
        {
            db = context;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            User user = db.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(User.Identity.Name));
            ViewBag.isUser = user.RoleId == 1;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
  
    }
}
