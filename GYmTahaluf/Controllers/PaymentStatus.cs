using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PaymentStatusesController : Controller
{
    private readonly ModelContext _context;
        
    public PaymentStatusesController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.PaymentStatuses.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var paymentStatus = await _context.PaymentStatuses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (paymentStatus == null)
        {
            return NotFound();
        }

        return View(paymentStatus);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Status,Description")] PaymentStatus paymentStatus)
    {
        if (ModelState.IsValid)
        {
            _context.Add(paymentStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(paymentStatus);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var paymentStatus = await _context.PaymentStatuses.FindAsync(id);
        if (paymentStatus == null)
        {
            return NotFound();
        }
        return View(paymentStatus);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Status,Description")] PaymentStatus paymentStatus)
    {
        if (id != paymentStatus.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(paymentStatus);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentStatusExists(paymentStatus.Id))
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
        return View(paymentStatus);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var paymentStatus = await _context.PaymentStatuses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (paymentStatus == null)
        {
            return NotFound();
        }

        return View(paymentStatus);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var paymentStatus = await _context.PaymentStatuses.FindAsync(id);
        _context.PaymentStatuses.Remove(paymentStatus);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentStatusExists(decimal id)
    {
        return _context.PaymentStatuses.Any(e => e.Id == id);
    }
}
