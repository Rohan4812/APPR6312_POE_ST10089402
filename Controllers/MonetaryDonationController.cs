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
       
    public class MonetaryDonationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonetaryDonationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: MonetaryDonation
        public async Task<IActionResult> Index()
        {
              return _context.MonetaryDon != null ? 
                          View(await _context.MonetaryDon.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MonetaryDon'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: MonetaryDonation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MonetaryDon == null)
            {
                return NotFound();
            }

            var monetaryDon = await _context.MonetaryDon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryDon == null)
            {
                return NotFound();
            }

            return View(monetaryDon);
        }
        [Authorize]

        // GET: MonetaryDonation/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> TotalMonetaryDonations()
        {
            return _context.MonetaryDon != null ?
                        View(await _context.MonetaryDon.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.MonetaryDon'  is null.");
        }

        // POST: MonetaryDonation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,Datedata")] MonetaryDon monetaryDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monetaryDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monetaryDon);
        }
        [Authorize(Roles = "Admin")]
        // GET: MonetaryDonation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MonetaryDon == null)
            {
                return NotFound();
            }

            var monetaryDon = await _context.MonetaryDon.FindAsync(id);
            if (monetaryDon == null)
            {
                return NotFound();
            }
            return View(monetaryDon);
        }
        [Authorize(Roles = "Admin")]
        // POST: MonetaryDonation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,Datedata")] MonetaryDon monetaryDon)
        {
            if (id != monetaryDon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monetaryDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonetaryDonExists(monetaryDon.Id))
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
            return View(monetaryDon);
        }
        [Authorize(Roles = "Admin")]
        // GET: MonetaryDonation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MonetaryDon == null)
            {
                return NotFound();
            }

            var monetaryDon = await _context.MonetaryDon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryDon == null)
            {
                return NotFound();
            }

            return View(monetaryDon);
        }
        [Authorize(Roles = "Admin")]
        // POST: MonetaryDonation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MonetaryDon == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MonetaryDon'  is null.");
            }
            var monetaryDon = await _context.MonetaryDon.FindAsync(id);
            if (monetaryDon != null)
            {
                _context.MonetaryDon.Remove(monetaryDon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonetaryDonExists(int id)
        {
          return (_context.MonetaryDon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
