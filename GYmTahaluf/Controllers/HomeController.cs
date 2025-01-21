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
            var home=_context.Pages.Where(x=>x.Title == "Home Page").FirstOrDefault();
            var testimonial = _context.Testimonials.Where(x=>x.Isapproved==true).ToList();
            var exercises = _context.Exercises.ToList();
            var about = _context.Pages.Where(x => x.Name == "about").First();

            // Create a ViewModel to hold both collections
            var results = new TestimonialTrainerViewModel
            {
                Home=home,
                About = about,
                Plans = plans,
                Testimonial = testimonial,
                Contact=contact,
                Exercise=exercises
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
            var myPlans = _context.SubscriptionPlans
                .Where(x => x.Schedules.Any(y => y.UserId == userId))
                .ToList();
            var contact = _context.Contacts.FirstOrDefault();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var trainners = _context.Users.Where(x => x.RoleId == 2).ToList();
            // Create a ViewModel to hold both collections
            var exercises=_context.Exercises.ToList();
            var about=_context.Pages.Where(x=>x.Name=="about").First();
            var home=_context.Pages.Where(x=>x.Title== "Member Home").FirstOrDefault();  
            var results = new MemberModel
            {About = about,
                User = user,
                Home=home,
                Contact = contact,
                Trainners = trainners,
                Plans = plans,
                Exercises = exercises,
                SubscripedPlan=myPlans
            }; return View(results);
        }


    }
}
