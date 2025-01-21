
using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ExercisesController : Controller
{
    private readonly ModelContext _context;

    public ExercisesController(ModelContext context)
    {
        _context = context;
    }

    // GET: Exercises
    public async Task<IActionResult> Index(int ?userId)
    {
        ViewBag.UserId = userId;

        return View(await _context.Exercises.Where(x=>x.Plan.TrainerId==userId).ToListAsync());
    }

    // GET: Exercises/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    // GET: Exercises/Create
    public IActionResult Create(int? userId)
    {
        if (userId == null)
        {
            return BadRequest("User ID is required.");
        }

        ViewBag.UserId = userId;

        // Create a list of SelectListItem for the dropdown
        ViewBag.PlanId = _context.SubscriptionPlans
            .Where(x => x.TrainerId == userId)
            .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.Id.ToString(), // Ensure x.Id is of type int or cast it to string
                Text = x.Id.ToString()  // Replace with a meaningful display name if available
            })
            .ToList();

        return View();
    }

    // POST: Exercises/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,Repetition,RestPeriod,Difficulty,Duration,PlanId")] Exercise exercise)
    {
       
            _context.Add(exercise);
            await _context.SaveChangesAsync();




        return RedirectToAction("Index", "TrainnerControllers", new { userId = exercise.Plan.TrainerId });
    }

    // GET: Exercises/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise =  _context.Exercises.Where(x=>x.Id==id).First();
        if (exercise == null)
        {
            return NotFound();
        }
        return View(exercise);
    }

    // POST: Exercises/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,Title,Description,Repetition,RestPeriod,Difficulty,Duration,PlanId")] Exercise exercise)
    {
        
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(exercise);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                int value = (int)exercise.Id;

                if (!ExerciseExists(value))
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
        return View(exercise);
    }

    // GET: Exercises/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    // POST: Exercises/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExerciseExists(int id)
    {
        return _context.Exercises.Any(e => e.Id == id);
    }
}
