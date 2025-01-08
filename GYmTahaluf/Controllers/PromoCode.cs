using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYmTahaluf.Controllers
{
    public class PromocodeController : Controller
    {
        private readonly ModelContext _context;

        public PromocodeController(ModelContext context)
        {
            _context = context;
        }

        // Index (View all Promocodes)
        public async Task<IActionResult> Index()
        {
            var promocodes = await _context.Promocodes.ToListAsync();
            return View(promocodes);
        }

        // Create View
        public IActionResult Create()
        {
            return View();
        }

        // Create Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Promocode promocode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocode);
        }

        // Edit View
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocode = await _context.Promocodes.FindAsync(id);
            if (promocode == null)
            {
                return NotFound();
            }
            return View(promocode);
        }

        // Edit Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, Promocode promocode)
        {
            if (id != promocode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocodeExists(promocode.Id))
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
            return View(promocode);
        }

        // Delete View
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocode = await _context.Promocodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocode == null)
            {
                return NotFound();
            }

            return View(promocode);
        }

        // Delete Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var promocode = await _context.Promocodes.FindAsync(id);
            _context.Promocodes.Remove(promocode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Check if Promocode exists
        private bool PromocodeExists(decimal id)
        {
            return _context.Promocodes.Any(e => e.Id == id);
        }
    }
}
