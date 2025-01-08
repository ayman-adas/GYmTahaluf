
using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SubscriptionPlansController : Controller
{
    private readonly ModelContext _context;

    public SubscriptionPlansController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.SubscriptionPlans.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subscriptionPlan = await _context.SubscriptionPlans
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subscriptionPlan == null)
        {
            return NotFound();
        }

        return View(subscriptionPlan);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,Duration")] SubscriptionPlan subscriptionPlan)
    {
        if (ModelState.IsValid)
        {
            _context.Add(subscriptionPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(subscriptionPlan);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subscriptionPlan = await _context.SubscriptionPlans.FindAsync(id);
        if (subscriptionPlan == null)
        {
            return NotFound();
        }
        return View(subscriptionPlan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Price,Duration")] SubscriptionPlan subscriptionPlan)
    {
        if (id != subscriptionPlan.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(subscriptionPlan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionPlanExists(subscriptionPlan.Id))
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
        return View(subscriptionPlan);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subscriptionPlan = await _context.SubscriptionPlans
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subscriptionPlan == null)
        {
            return NotFound();
        }

        return View(subscriptionPlan);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var subscriptionPlan = await _context.SubscriptionPlans.FindAsync(id);
        _context.SubscriptionPlans.Remove(subscriptionPlan);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SubscriptionPlanExists(decimal id)
    {
        return _context.SubscriptionPlans.Any(e => e.Id == id);
    }
}
