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
    [HttpPost]
    public async Task<IActionResult> Create(Testimonial testimonial)
    {
        // Ensure the Testimonial object is valid and contains the required data
        if (testimonial == null)
        {
            return BadRequest("Testimonial data is missing.");
        }

        // Set the rated time and save the testimonial
        testimonial.RatedTime = DateTime.Now;

        if (testimonial.imageFile != null)
        {
            // Process the image upload if any
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", testimonial.imageFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await testimonial.imageFile.CopyToAsync(stream);
            }

            // Save the image file name in the database
            testimonial.Image = testimonial.imageFile.FileName; // Assign file name to the Image property
        }

        // Add the testimonial to the context
        _context.Add(testimonial);
        await _context.SaveChangesAsync();

        // Redirect to the MemberHome page
        return RedirectToAction("MemberHome", "Home", new { userId = testimonial.UserId });
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
