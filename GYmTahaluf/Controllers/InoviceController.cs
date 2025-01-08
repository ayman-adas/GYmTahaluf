using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class InvoicesController : Controller
{
    private readonly ModelContext _context;

    public InvoicesController(ModelContext context)
    {
        _context = context;
    }

    // GET: Invoices
    public async Task<IActionResult> Index()
    {
        return View(await _context.Invoices.ToListAsync());
    }

    // GET: Invoices/Details/5
    public async Task<IActionResult> Details(decimal? id)  // Using decimal for id
    {
        if (id == null)
        {
            return NotFound();
        }

        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(m => m.Id == id);
        if (invoice == null)
        {
            return NotFound();
        }

        return View(invoice);
    }

    // GET: Invoices/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Invoices/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Amount,Date")] Invoice invoice)
    {
        if (ModelState.IsValid)
        {
            _context.Add(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(invoice);
    }

    // GET: Invoices/Edit/5
    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        return View(invoice);
    }

    // POST: Invoices/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Amount,Date")] Invoice invoice)
    {
        if (id != invoice.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(invoice.Id))
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
        return View(invoice);
    }

    // GET: Invoices/Delete/5
    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(m => m.Id == id);
        if (invoice == null)
        {
            return NotFound();
        }

        return View(invoice);
    }

    // POST: Invoices/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InvoiceExists(decimal id)
    {
        return _context.Invoices.Any(e => e.Id == id);
    }
}
