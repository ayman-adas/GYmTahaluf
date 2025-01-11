
using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

public class SubscriptionPlanController : Controller
{
    private readonly ModelContext _context;

    public SubscriptionPlanController(ModelContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int id,int ?userId)
    {
        var result = _context.SubscriptionPlans.FirstOrDefault(x => x.Id == id);
        ViewBag.UserId=userId;
        return View(result);
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
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(
     string startDay,
     string startTime,
     string visaDetails,
     string? promoCode,
     int planId,
     int userId)
    {
      
         
            // Map form data to subscription model
            var subscription = new Schedule
            {
                StartDay = _context.Days.Where(x=>x.DaysName==startDay).Single().Id,
                StartTime = TimeSpan.TryParse(startTime, out var parsedTime)
                            ? DateTime.Today.Add(parsedTime)
                            : null,
                PlanId = planId, // Replace with the appropriate plan ID logic
                UserId = userId, // Replace with the logged-in user ID
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Isavailable = true,
            };

            // Save to database
            _context.Schedules.Add(subscription);
            _context.SaveChanges();

            // Handle promo code (if provided)

                var promocodes = _context.Promocodes.FirstOrDefault(p => p.Code == promoCode);
        var plan=_context.SubscriptionPlans.First(p => p.Id == planId);
        var user =_context.Users.First(p => p.Id == userId);
                var payment = new Payment
                {
                    PlanId = plan.Id,
                    UserId = userId,
                    Amount = plan.Price,
                    PaymentStatus = "Completed", // Change based on your logic
                    TransactionDate = DateTime.Now,
                    TransactionRef = Guid.NewGuid().ToString(), // Unique reference for transaction
                    IsUsedPromo = !string.IsNullOrEmpty(promoCode),
                    Promocode = string.IsNullOrEmpty(promoCode) ? null : promocodes.Id
                };

                // Save to the database
                _context.Payments.Add(payment);
                _context.SaveChanges();
        var payments = _context.Payments
            .OrderByDescending(p => p.TransactionDate) // Replace 'Date' with your column
            .FirstOrDefault();


        // Save updated subscription with promo code
        RedirectToAction("CompletePayment", "Invoices", new { payments.Id, plan.Price, user.UserName, user.Email });
        _context.SaveChanges();
            TempData["SuccessMessage"] = "Subscription created successfully!";
            return RedirectToAction("MemberHome", "Home", new { userId = userId });
        }

    public IActionResult Create()
    {
        return View();
    }

    // Process the form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SubscriptionPlan model)
    {
        if (ModelState.IsValid)
        {
            var newPlan = new SubscriptionPlan
            {
                TrainerId = model.TrainerId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Price = model.Price,
                Descriptions = model.Descriptions,
                Title = model.Title,
                Goal = model.Goal,
                DaysInWeek = model.DaysInWeek,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Isvisible = true // Default value
            };

            _context.SubscriptionPlans.Add(newPlan);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to a list of plans
        }

        return View(model); // Redisplay the form with validation errors
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
