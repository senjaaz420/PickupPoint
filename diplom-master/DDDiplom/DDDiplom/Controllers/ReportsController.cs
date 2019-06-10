using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDDiplom.Controllers
{
    public class ReportsController : Controller
    {
        
        private readonly DDDiplomContext _context;

        public ReportsController(DDDiplomContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dDDiplomContext = _context.Orders.Include(o => o.Client).Include(o => o.WorkPlace).Where(o => o.IsPaid == "да");
            return View(await dDDiplomContext.ToListAsync());
        }
    }
}