using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDDiplom.Models;

namespace DDDiplom.Controllers
{
    public class WorkPlacesController : Controller
    {
        private readonly DDDiplomContext _context;

        public WorkPlacesController(DDDiplomContext context)
        {
            _context = context;
        }

        // GET: WorkPlaces
        public async Task<IActionResult> Index()
        {
            var dDDiplomContext = _context.WorkPlaces.Include(w => w.Address);
            return View(await dDDiplomContext.ToListAsync());
        }

        // GET: WorkPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlace = await _context.WorkPlaces
                .Include(w => w.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workPlace == null)
            {
                return NotFound();
            }

            return View(workPlace);
        }

        // GET: WorkPlaces/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id");
            return View();
        }

        // POST: WorkPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UsersId,AddressId")] WorkPlace workPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", workPlace.AddressId);
            return View(workPlace);
        }

        // GET: WorkPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlace = await _context.WorkPlaces.FindAsync(id);
            if (workPlace == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", workPlace.AddressId);
            return View(workPlace);
        }

        // POST: WorkPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UsersId,AddressId")] WorkPlace workPlace)
        {
            if (id != workPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkPlaceExists(workPlace.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", workPlace.AddressId);
            return View(workPlace);
        }

        // GET: WorkPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlace = await _context.WorkPlaces
                .Include(w => w.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workPlace == null)
            {
                return NotFound();
            }

            return View(workPlace);
        }

        // POST: WorkPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workPlace = await _context.WorkPlaces.FindAsync(id);
            _context.WorkPlaces.Remove(workPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkPlaceExists(int id)
        {
            return _context.WorkPlaces.Any(e => e.Id == id);
        }
    }
}
