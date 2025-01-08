using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RolesController : Controller
{
    private readonly ModelContext _context;

    public RolesController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Roles.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Role role)
    {
        if (ModelState.IsValid)
        {
            _context.Add(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(role);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name")] Role role)
    {
        if (id != role.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(role.Id))
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
        return View(role);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var role = await _context.Roles.FindAsync(id);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RoleExists(decimal id)
    {
        return _context.Roles.Any(e => e.Id == id);
    }
}
