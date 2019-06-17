using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDDiplom.Models;
using DDDiplom.ViewModels;

namespace DDDiplom.Controllers
{
    public class WorkPlacesController : Controller
    {
        private readonly DDDiplomContext _context;

        public WorkPlacesController(DDDiplomContext context )
        {
            _context = context;
        }

        // GET: WorkPlaces
        public async Task<IActionResult> Index()
        {
            var dDDiplomContext = _context.WorkPlaces.Include(u => u.Address);
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
            return View();
        }

        // POST: WorkPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] WorkPlace workPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workPlace);
        }

        // GET: WorkPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var _workPlace = await _context.WorkPlaces.FindAsync(id);
            var _address = await _context.Addresses.SingleOrDefaultAsync(p => p.Id == _workPlace.AddressId);

            WorkAdressViewModel model = new WorkAdressViewModel
            {
                workPlace = _workPlace,
                address = _address
            };

            if (_workPlace == null && _address == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: WorkPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WorkPlace workPlace, [Bind("Id,City,Street,BuildingNumber")] Address address)
        {
            if (id != workPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    WorkPlace _workPlace = _context.WorkPlaces.SingleOrDefault(p=>p.Id == workPlace.Id);
                    _workPlace.Name = workPlace.Name;             
                    // _workPlace.
                    Address _address = new Address
                    {
                        Id = _workPlace.AddressId,
                        City = address.City,
                        Street = address.Street,
                        BuildingNumber = address.BuildingNumber
                    };
                    _context.Update(_address);
                    _context.Update(_workPlace);
                    
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
            return View(_context);
        }

        // GET: WorkPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlace = await _context.WorkPlaces
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
