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
    public class AddGoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddGoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddGoods
        public async Task<IActionResult> Index()
        {
              return _context.AddGoods != null ? 
                          View(await _context.AddGoods.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AddGoods'  is null.");
        }

        // GET: AddGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddGoods == null)
            {
                return NotFound();
            }

            var addGoods = await _context.AddGoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addGoods == null)
            {
                return NotFound();
            }

            return View(addGoods);
        }
        [Authorize]

        // GET: AddGoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Goods")] AddGoods addGoods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addGoods);
        }

        // GET: AddGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddGoods == null)
            {
                return NotFound();
            }

            var addGoods = await _context.AddGoods.FindAsync(id);
            if (addGoods == null)
            {
                return NotFound();
            }
            return View(addGoods);
        }

        // POST: AddGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Goods")] AddGoods addGoods)
        {
            if (id != addGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddGoodsExists(addGoods.Id))
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
            return View(addGoods);
        }

        // GET: AddGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddGoods == null)
            {
                return NotFound();
            }

            var addGoods = await _context.AddGoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addGoods == null)
            {
                return NotFound();
            }

            return View(addGoods);
        }

        // POST: AddGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddGoods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AddGoods'  is null.");
            }
            var addGoods = await _context.AddGoods.FindAsync(id);
            if (addGoods != null)
            {
                _context.AddGoods.Remove(addGoods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddGoodsExists(int id)
        {
          return (_context.AddGoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
