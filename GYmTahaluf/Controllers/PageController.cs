using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PageController : Controller
{
    private readonly ModelContext _context;

    public PageController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Pages.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var page = await _context.Pages
            .FirstOrDefaultAsync(m => m.Id == id);
        if (page == null)
        {
            return NotFound();
        }

        return View(page);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,Title,Name,Content")] Page page)
    {
        if (ModelState.IsValid)
        {
            _context.Add(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(page);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var page = await _context.Pages.FindAsync(id);
        if (page == null)
        {
            return NotFound();
        }
        return View(page);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Title,Name,Content")] Page page)
    {


        try
        {
            var Page = _context.Pages.Where(x => x.Id == id).First();
            Page.Title = page.Title;
            Page.Content = page.Content;
            Page.Name = page.Name;
            _context.Update(Page);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PageExists(page.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }

        }
        return View("Index");

    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var page = await _context.Pages
            .FirstOrDefaultAsync(m => m.Id == id);
        if (page == null)
        {
            return NotFound();
        }

        return View(page);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var page = await _context.Pages.FindAsync(id);
        _context.Pages.Remove(page);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PageExists(decimal id)
    {
        return _context.Pages.Any(e => e.Id == id);
    }
}
