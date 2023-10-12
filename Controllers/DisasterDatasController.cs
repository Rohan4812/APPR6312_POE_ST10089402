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
    
    public class DisasterDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisasterDatasController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]

        // GET: DisasterDatas
        public async Task<IActionResult> Index()
        {
              return _context.DisasterData != null ? 
                          View(await _context.DisasterData.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DisasterData'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DisasterData == null)
            {
                return NotFound();
            }

            var disasterData = await _context.DisasterData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterData == null)
            {
                return NotFound();
            }

            return View(disasterData);
        }
        [Authorize]
        // GET: DisasterDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisasterDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Discription,Location,AidType,StartDate,EndDate")] DisasterData disasterData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disasterData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasterData);
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DisasterData == null)
            {
                return NotFound();
            }

            var disasterData = await _context.DisasterData.FindAsync(id);
            if (disasterData == null)
            {
                return NotFound();
            }
            return View(disasterData);
        }
        [Authorize(Roles = "Admin")]
        // POST: DisasterDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Discription,Location,AidType,StartDate,EndDate")] DisasterData disasterData)
        {
            if (id != disasterData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasterData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterDataExists(disasterData.Id))
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
            return View(disasterData);
        }
        [Authorize(Roles = "Admin")]
        // GET: DisasterDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DisasterData == null)
            {
                return NotFound();
            }

            var disasterData = await _context.DisasterData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterData == null)
            {
                return NotFound();
            }

            return View(disasterData);
        }
        [Authorize(Roles = "Admin")]
        // POST: DisasterDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DisasterData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DisasterData'  is null.");
            }
            var disasterData = await _context.DisasterData.FindAsync(id);
            if (disasterData != null)
            {
                _context.DisasterData.Remove(disasterData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterDataExists(int id)
        {
          return (_context.DisasterData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
