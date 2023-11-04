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
    public class DisasterGoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisasterGoodsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterGoods
        public async Task<IActionResult> Index()
        {
              return _context.DisasterGoods != null ? 
                          View(await _context.DisasterGoods.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DisasterGoods'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DisasterGoods == null)
            {
                return NotFound();
            }

            var disasterGoods = await _context.DisasterGoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterGoods == null)
            {
                return NotFound();
            }

            return View(disasterGoods);
        }
        [Authorize]

        // GET: DisasterGoods/Create
        public IActionResult Create()
        {
            DisasterGoodsCreateModel model = new DisasterGoodsCreateModel();
            model.DisasterGoods = new DisasterGoods();
            List<SelectListItem> disasterGoods = _context.DisasterData
                .OrderBy(n => n.Name)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.Name,
                    Text = n.Name
                }).ToList();

            model.DisasterData = disasterGoods;
            return View(model);
        }

        public async Task<IActionResult> TotalDisasterGoods()
        {
            return _context.DisasterGoods != null ?
                        View(await _context.DisasterGoods.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.DisasterGoods'  is null.");
        }

        // POST: DisasterGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Disaster,Category,NumOfItems,Description")] DisasterGoods disasterGoods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disasterGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasterGoods);
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DisasterGoods == null)
            {
                return NotFound();
            }

            var disasterGoods = await _context.DisasterGoods.FindAsync(id);
            if (disasterGoods == null)
            {
                return NotFound();
            }
            return View(disasterGoods);
        }
        [Authorize(Roles = "Admin")]
        // POST: DisasterGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Disaster,Category,NumOfItems,Description")] DisasterGoods disasterGoods)
        {
            if (id != disasterGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasterGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterGoodsExists(disasterGoods.Id))
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
            return View(disasterGoods);
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DisasterGoods == null)
            {
                return NotFound();
            }

            var disasterGoods = await _context.DisasterGoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterGoods == null)
            {
                return NotFound();
            }

            return View(disasterGoods);
        }
        [Authorize(Roles = "Admin")]
        // POST: DisasterGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DisasterGoods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DisasterGoods'  is null.");
            }
            var disasterGoods = await _context.DisasterGoods.FindAsync(id);
            if (disasterGoods != null)
            {
                _context.DisasterGoods.Remove(disasterGoods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterGoodsExists(int id)
        {
          return (_context.DisasterGoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
