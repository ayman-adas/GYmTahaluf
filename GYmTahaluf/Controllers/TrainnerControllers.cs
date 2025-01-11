using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYmTahaluf.Controllers
{
    public class TrainnerControllers : Controller
    {
        public ModelContext _context { get; set; }
        public TrainnerControllers(ModelContext modelContext)
        {
            _context = modelContext;
        }
        public IActionResult Index(int? userId)
        {
            var plans = _context.SubscriptionPlans
                .Where(x => x.TrainerId == userId)
                .ToList();
            var members = _context.Schedules
                .Where(x => x.Plan.TrainerId == userId)
                .Select(x => x.User) // Assuming Schedule has a User navigation property
                .ToList();
            var totalPayment = _context.Payments.Where(x => x.Plan.TrainerId == userId).Sum(x => x.Amount);
            var result = new TrainerDashboard
            {
                Plans = plans,
                Members = members,
                TotalPayment = totalPayment
            };
            return View(result);
        }

        public IActionResult Search()
        {
            var result = _context.SubscriptionPlans.Include(x => x.Trainer).Include(x => x.Payments).ToList();
            return View(result);
        }
    }
}
    
