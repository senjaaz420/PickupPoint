using DDDiplom.Models;
using DDDiplom.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DIPLOM.Controllers
{
    public class AccountController : Controller
    {
        private DDDiplomContext db;
        public AccountController(DDDiplomContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = await db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user != null)
            {
                try
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
            return View(model);


        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);

        //    if (user == null)
        //    {
        //        // добавляем пользователя в бд
        //        user = new User { Login = model.Login, Password = model.Password };
        //        Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
        //        if (userRole != null)
        //            user.Role = userRole;

        //        db.Users.Add(user);
        //        await db.SaveChangesAsync();

        //        await Authenticate(user); // аутентификация

        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}

        public async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
      
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
