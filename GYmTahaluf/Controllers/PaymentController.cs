
using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class PaymentsController : Controller
{
    private readonly ModelContext _context;

    public PaymentsController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Payments.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payments
            .FirstOrDefaultAsync(m => m.Id == id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    public IActionResult Create()
    {
        ViewData["PaymentStatusId"] = new SelectList(_context.PaymentStatuses, "Id", "Status");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Amount,Date,UserId")] Payment payment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(payment);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        ViewData["PaymentStatusId"] = new SelectList(_context.PaymentStatuses, "Id", "Status", payment.PaymentStatus);

        return View(payment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Amount,Date,UserId", "PaymentStatusId")] Payment payment)
    {
        if (id != payment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(payment.Id))
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
        return View(payment);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payments
            .FirstOrDefaultAsync(m => m.Id == id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var payment = await _context.Payments.FindAsync(id);
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentExists(decimal id)
    {
        return _context.Payments.Any(e => e.Id == id);
    }
}
