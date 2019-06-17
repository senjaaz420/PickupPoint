﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDDiplom.Models;
using DDDiplom.ViewModels.Account;

namespace DDDiplom.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly DDDiplomContext _context;

        public UserProfilesController(DDDiplomContext context)
        {
            _context = context;
        }

        // GET: UserProfiles
        public ActionResult Index(string AddressesFilter = "all")
        {
            List<string> addresses =  _context.UserProfiles.Select(x => x.WorkPlace.Address.Street).Distinct().ToList();
            ViewBag.Addresses = addresses;

            List<UserProfile> dDDiplomContext = _context.UserProfiles
                .Include(u => u.User)
                .Include(u => u.WorkPlace)
                .Include(u => u.WorkPlace.Address)
                .Where(x => AddressesFilter != "all" ? x.WorkPlace.Address.Street == AddressesFilter : true).ToList();

            return View(dDDiplomContext);
        }

        // GET: UserProfiles/Details/5

        // GET: UserProfiles/Create
        public IActionResult Register()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewBag.WorkPlace = new SelectList(_context.WorkPlaces, "Name", "Name");
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(UserProfile userProfile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_context.Add(userProfile);
        //        _context.Users.Add(new User
        //        {
        //            Login = userProfile.User.Login,
        //            Password = userProfile.User.Password,
        //            RoleId = 2
        //        });

        //        _context.UserProfiles.Add(new UserProfile
        //        {
        //            Name = userProfile.Name,
        //            Surname = userProfile.Surname,
        //            Patronymic = userProfile.Patronymic,
        //            Experience = userProfile.Experience,
        //            UserId = _context.Users.LastOrDefault().Id,
        //            WorkPlaceId = userProfile.WorkPlace.Id
        //        });
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(userProfile);
        //}






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string hram)
        {
            Random rnd = new Random();
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = new User
            {
              // Id = rnd.Next(1, 1000000000),
               Login = model.Login,
               Password = model.Password,
               RoleId = 2     
            };

            _context.Add(user);
            await _context.SaveChangesAsync();
            List<User> dbUser = _context.Users.ToList();
            WorkPlace workPlace = _context.WorkPlaces.SingleOrDefault(p=>p.Name == hram);
            var userProfile = new UserProfile
            {
                //Id = rnd.Next(1,1000000000),
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Experience = model.Experience,
                UserId = dbUser.Count(),
                WorkPlaceId = workPlace.Id
            };

            _context.Add(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "UserProfiles");
            //    }
            //        else
            //        {
            //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            //        }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", model.UserId);
            //ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", model.WorkPlaceId);
            //        return View(model);
        }










        // GET: UserProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", userProfile.WorkPlaceId);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Patronymic,Experience,WorkPlaceId")] UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", userProfile.WorkPlaceId);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .Include(u => u.User)
                .Include(u => u.WorkPlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.Id == id);
        }
    }
}
