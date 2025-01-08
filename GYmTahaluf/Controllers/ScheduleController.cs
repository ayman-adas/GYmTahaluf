using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SchedulesController : Controller
{
    private readonly ModelContext _context;

    public SchedulesController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Schedules.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(m => m.Id == id);
        if (schedule == null)
        {
            return NotFound();
        }

        return View(schedule);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,DayOfWeek")] Schedule schedule)
    {
        if (ModelState.IsValid)
        {
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(schedule);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var schedule = await _context.Schedules.FindAsync(id);
        if (schedule == null)
        {
            return NotFound();
        }
        return View(schedule);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,StartTime,EndTime,DayOfWeek")] Schedule schedule)
    {
        if (id != schedule.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(schedule);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(schedule.Id))
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
        return View(schedule);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(m => m.Id == id);
        if (schedule == null)
        {
            return NotFound();
        }

        return View(schedule);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var schedule = await _context.Schedules.FindAsync(id);
        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ScheduleExists(decimal id)
    {
        return _context.Schedules.Any(e => e.Id == id);
    }
}
