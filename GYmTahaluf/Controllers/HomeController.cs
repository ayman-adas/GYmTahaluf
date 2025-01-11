using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GYM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ModelContext context)
        {
            _logger = logger;
            _context= context;
        }

        public IActionResult Index()
        {
             var plans = _context.SubscriptionPlans.ToList();
            var contact = _context.Contacts.FirstOrDefault();

            var testimonial = _context.Testimonials.Where(x=>x.Isapproved==true).ToList();
            // Create a ViewModel to hold both collections
            var results = new TestimonialTrainerViewModel
            {
                Plans = plans,
                Testimonial = testimonial,
                Contact=contact
            }; return View(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult MemberHome(int?userId)
        {
            var plans = _context.SubscriptionPlans.ToList();
            var contact = _context.Contacts.FirstOrDefault();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var trainners = _context.Users.Where(x => x.RoleId == 2).ToList();
            // Create a ViewModel to hold both collections
            var exercises=_context.Exercises.ToList();
            var results = new MemberModel
            {
                User = user,
                Contact = contact,
                Trainners = trainners,
                Plans = plans,
                Exercises = exercises
            }; return View(results);
        }


    }
}
