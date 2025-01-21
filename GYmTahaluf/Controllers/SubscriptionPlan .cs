
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

    public async Task<IActionResult> View1(int? userId)
    {
        ViewBag.UserId = userId;

        var result = _context.SubscriptionPlans.Where(x => x.TrainerId == userId).ToList();
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
    public async Task<IActionResult> MyPlan(decimal? id,decimal? userId)
    {
        if (id == null)
        {
            return NotFound("Plan ID cannot be null.");
        }

        // Fetch the subscription plan by ID from the database
        var subscriptionPlan =_context.SubscriptionPlans
            .First(p => p.Id == id);
        var sechudles=_context.Schedules.Where(x => x.UserId == userId).FirstOrDefault();
        var excercise=_context.Exercises.Where(x=>x.PlanId==id).ToList();
        var subscribedPlan = new SubScripedPlan
        {
            Exercises = excercise,
            Plan = subscriptionPlan,
            Schedule = sechudles
        };


        if (subscriptionPlan == null)
        {
            return NotFound($"No subscription plan found with ID {id}.");
        }

        // Return the view with the subscription plan data
        return View(subscribedPlan);
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
                        Amount = plan.Price-((promocodes?.Percent??0)*plan.Price),
                        PaymentStatus = "Completed", // Change based on your logic
                        TransactionDate = DateTime.Now,
                        TransactionRef = Guid.NewGuid().ToString(), // Unique reference for transaction
                        IsUsedPromo = !string.IsNullOrEmpty(promoCode),
                        Promocode = string.IsNullOrEmpty(promoCode) ? null : promocodes.Id
                    };

        // Save to the database
        _context.Payments.Add(payment);
                    _context.SaveChanges();

        var lastPayment = _context.Payments
    .OrderByDescending(p => p.TransactionDate) // Assuming TransactionDate is a DateTime field
    .First();

        return RedirectToAction("CompletePayment", "Invoices", new { paymentId=lastPayment.Id, amount = plan.Price, userName=user.UserName, email= user.Email });

        }
    [HttpGet]
    public async Task<IActionResult> Create(int?userId)
    {
        ViewBag.UserId = userId;

        return View();
    }

    [HttpPost]
    public IActionResult Create(SubscriptionPlan model)
    {
        if (ModelState.IsValid)
        {
            model.TrainerId = HttpContext.Session.GetInt32("TrainnerId");



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

            return RedirectToAction("Index", "TrainnerControllers", new { userId = model.TrainerId });
        }

        return RedirectToAction("Index", "TrainnerControllers", new { userId = model.TrainerId });
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
    public async Task<IActionResult> Edit(decimal? id)
    {
        var result=_context.SubscriptionPlans.Where(x=>x.Id == id).SingleOrDefault();
        return View(result);
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
