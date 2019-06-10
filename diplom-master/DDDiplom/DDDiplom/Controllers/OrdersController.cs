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
    public class OrdersController : Controller
    {
        private readonly DDDiplomContext _context;

        public OrdersController(DDDiplomContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Report()
        {
            var dDDiplomContext = _context.Orders.Where(o => o.IsPaid == "да").Include(o => o.Client);
            return View(await dDDiplomContext.ToListAsync());
        }
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var dDDiplomContext = _context.Orders.Include(o => o.Client).Include(o => o.WorkPlace).Where(o => o.IsPaid =="нет");
            return View(await dDDiplomContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.WorkPlace)
                .Include(o => o.Client)
                .Include(o =>o.OrderProducts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id");
            return View();
        }

        public IActionResult Export(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", order.WorkPlaceId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Export(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    order.IsPaid = "да";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", order.WorkPlaceId);
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsPaid,Summary,OrderTime,WorkPlaceId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", order.WorkPlaceId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", order.WorkPlaceId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsPaid,Summary,OrderTime,WorkPlaceId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["WorkPlaceId"] = new SelectList(_context.WorkPlaces, "Id", "Id", order.WorkPlaceId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.WorkPlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
