using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Data;
using APPR6312_POE.Models;
using Microsoft.AspNetCore.Authorization;

namespace APPR6312_POE.Controllers
{
    public class GoodsDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoodsDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: GoodsDonations
        public async Task<IActionResult> Index()
        {
              return _context.Goods != null ? 
                          View(await _context.Goods.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Goods'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: GoodsDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Goods == null)
            {
                return NotFound();
            }

            var goodsDonations = await _context.Goods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodsDonations == null)
            {
                return NotFound();
            }

            return View(goodsDonations);
        }
        [Authorize]

        // GET: GoodsDonations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GoodsDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,NumOfItems,Description,GoodsDate")] GoodsDonations goodsDonations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goodsDonations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goodsDonations);
        }
        [Authorize(Roles = "Admin")]
        // GET: GoodsDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Goods == null)
            {
                return NotFound();
            }

            var goodsDonations = await _context.Goods.FindAsync(id);
            if (goodsDonations == null)
            {
                return NotFound();
            }
            return View(goodsDonations);
        }
        [Authorize(Roles = "Admin")]
        // POST: GoodsDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,NumOfItems,Description,GoodsDate")] GoodsDonations goodsDonations)
        {
            if (id != goodsDonations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodsDonations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsDonationsExists(goodsDonations.Id))
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
            return View(goodsDonations);
        }
        [Authorize(Roles = "Admin")]
        // GET: GoodsDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Goods == null)
            {
                return NotFound();
            }

            var goodsDonations = await _context.Goods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodsDonations == null)
            {
                return NotFound();
            }

            return View(goodsDonations);
        }
        [Authorize(Roles = "Admin")]
        // POST: GoodsDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Goods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Goods'  is null.");
            }
            var goodsDonations = await _context.Goods.FindAsync(id);
            if (goodsDonations != null)
            {
                _context.Goods.Remove(goodsDonations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsDonationsExists(int id)
        {
          return (_context.Goods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
