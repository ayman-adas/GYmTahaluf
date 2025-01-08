using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TestimonialController : Controller
{
    private readonly ModelContext _context;

    public TestimonialController(ModelContext context)
    {
        _context = context;
    }

    // Index - Show all testimonials
    public IActionResult Index()
    {
        var testimonials = _context.Testimonials.Include(t => t.User).ToList();
        return View(testimonials);
    }

    // Edit - Edit testimonial details
    public IActionResult Edit(decimal id)
    {
        var testimonial = _context.Testimonials.Find(id);
        if (testimonial == null)
        {
            return NotFound();
        }
        return View(testimonial);
    }

    [HttpPost]
    public IActionResult Edit(decimal id, Testimonial testimonial)
    {
        if (id != testimonial.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(testimonial);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Testimonials.Any(t => t.Id == testimonial.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(testimonial);
    }

    // Details - Show testimonial details
    public IActionResult Details(decimal id)
    {
        var testimonial = _context.Testimonials.Include(t => t.User).FirstOrDefault(t => t.Id == id);
        if (testimonial == null)
        {
            return NotFound();
        }
        return View(testimonial);
    }

    // Delete - Confirm deletion of testimonial
    public IActionResult Delete(decimal id)
    {
        var testimonial = _context.Testimonials.Include(t => t.User).FirstOrDefault(t => t.Id == id);
        if (testimonial == null)
        {
            return NotFound();
        }
        return View(testimonial);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(decimal id)
    {
        var testimonial = _context.Testimonials.Find(id);
        if (testimonial != null)
        {
            _context.Testimonials.Remove(testimonial);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
