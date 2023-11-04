using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Data;
using APPR6312_POE.Models;
using APPR6312_POE.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace APPR6312_POE.Controllers
{
    public class DisasterMoneyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisasterMoneyController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterMoney
        public async Task<IActionResult> Index()
        {
              return _context.DisasterMoney != null ? 
                          View(await _context.DisasterMoney.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DisasterMoney'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterMoney/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DisasterMoney == null)
            {
                return NotFound();
            }

            var disasterMoney = await _context.DisasterMoney
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterMoney == null)
            {
                return NotFound();
            }

            return View(disasterMoney);
        }
        [Authorize]

        // GET: DisasterMoney/Create
        public IActionResult Create()
        {
            DisasterMoneyCreateModel model = new DisasterMoneyCreateModel();
            model.DisasterMoney = new DisasterMoney();
            List<SelectListItem> disasterMoney = _context.DisasterData
                .OrderBy(n => n.Name)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.Name,
                    Text = n.Name
                }).ToList();

            model.DisasterData = disasterMoney;
            return View(model);
        }

        public async Task<IActionResult> TotalDisasterMoney()
        {
            return _context.DisasterMoney != null ?
                        View(await _context.DisasterMoney.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.DisasterMoney'  is null.");
        }

        // POST: DisasterMoney/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Disaster,Amount")] DisasterMoney disasterMoney)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disasterMoney);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasterMoney);
        }


        [Authorize(Roles = "Admin")]
        // GET: DisasterMoney/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DisasterMoney == null)
            {
                return NotFound();
            }

            var disasterMoney = await _context.DisasterMoney.FindAsync(id);
            if (disasterMoney == null)
            {
                return NotFound();
            }
            return View(disasterMoney);
        }
        [Authorize(Roles = "Admin")]
        // POST: DisasterMoney/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Disaster,Amount")] DisasterMoney disasterMoney)
        {
            if (id != disasterMoney.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasterMoney);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterMoneyExists(disasterMoney.Id))
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
            return View(disasterMoney);
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterMoney/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DisasterMoney == null)
            {
                return NotFound();
            }

            var disasterMoney = await _context.DisasterMoney
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterMoney == null)
            {
                return NotFound();
            }

            return View(disasterMoney);
        }
        [Authorize(Roles = "Admin")]
        // POST: DisasterMoney/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DisasterMoney == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DisasterMoney'  is null.");
            }
            var disasterMoney = await _context.DisasterMoney.FindAsync(id);
            if (disasterMoney != null)
            {
                _context.DisasterMoney.Remove(disasterMoney);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterMoneyExists(int id)
        {
          return (_context.DisasterMoney?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
