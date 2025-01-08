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
             var testomonials=_context.Testimonials.ToList();
            var trainers=_context.Users.Where(x=>x.RoleId==2).ToList();
            // Create a ViewModel to hold both collections
            var results = new TestimonialTrainerViewModel
            {
                Testimonials = testomonials,
                Trainers = trainers
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


    }
}
