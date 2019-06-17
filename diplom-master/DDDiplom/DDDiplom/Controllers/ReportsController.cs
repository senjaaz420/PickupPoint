using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace DDDiplom.Controllers
{
    public class ReportsController : Controller
    {
        
        private readonly DDDiplomContext _context;
        private DateTime StartDate;

        public ActionResult GeneratePDF()
        {
            var dDDiplomContext = _context.Orders
                   .Include(o => o.Client)
                   .Include(o => o.WorkPlace)
                   .Where(o => o.IsPaid == "да");
            return View(dDDiplomContext);
        }
        public ReportsController(DDDiplomContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string startDate ="", string endDate="")
        {
            var dDDiplomContext = _context.Orders
                    .Include(o => o.Client)
                    .Include(o => o.WorkPlace)
                    .Where(o => o.IsPaid == "да");


            if (startDate != "" && endDate != "")
                dDDiplomContext = dDDiplomContext.Where(o => o.OrderTime >= DateTime.Parse(startDate) && o.OrderTime <= DateTime.Parse(endDate));
            

            return View(await dDDiplomContext.ToListAsync());
        }
        public ActionResult Export()
        {
            var dDDiplomContext = _context.Orders
                   .Include(o => o.Client)
                   .Include(o => o.WorkPlace)
                   .Where(o => o.IsPaid == "да");

            return new ViewAsPdf("Export", dDDiplomContext.ToList());
        }


    }
}