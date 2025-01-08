using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restruarntmvc.Models;
using System.Drawing.Text;

namespace restruarntmvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var registeredMembersCount = _context.Users.Count();
            var activeSubscriptionsCount = _context.SubscriptionPlans.Count(s => s.Isvisible??false);
            var totalRevenue = _context.Payments.Sum(p => p.Amount);
            var monthlySubscriptionReport = GetMonthlySubscriptionReport();
            var annualSubscriptionReport = GetAnnualSubscriptionReport();
            var plans=_context.SubscriptionPlans.ToList();
            var payment = _context.Payments.ToList();
            var users = _context.Users.ToList();
            var testomonial = _context.Testimonials.ToList();

            var model = new DashboardViewModel
            {
                RegisteredMembersCount = registeredMembersCount,
                ActiveSubscriptionsCount = activeSubscriptionsCount,
                TotalRevenue = totalRevenue,
                MonthlySubscriptionReport = monthlySubscriptionReport,
                AnnualSubscriptionReport = annualSubscriptionReport,
                Item1 =plans,
                Item2=payment,
                Item3 =users,
                Item4=testomonial,

                // Add any other data needed for your view
            };

            return View(model);
        }

        private List<MonthlyReport> GetMonthlySubscriptionReport() => _context.Payments
                .GroupBy(p => new { p.TransactionDate.Value.Month, p.TransactionDate.Value.Year })
                .Select(g => new MonthlyReport
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalAmount = g.Sum(p => p.Amount)
                }).ToList();

        private List<AnnualReport> GetAnnualSubscriptionReport()
        {
            return _context.Payments
                .GroupBy(p => p.TransactionDate.Value.Year)
                .Select(g => new AnnualReport
                {
                    Year = g.Key,
                    TotalAmount = g.Sum(p => p.Amount)
                }).ToList();
        }

        public IActionResult JoinTables()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();
            var customers = _context.Customers.ToList();
            var productCustomer = _context.ProductCustomers.ToList();
            var Result = from c in customers
                         join pc in productCustomer on c.CustomerId equals pc.CustomerId
                         join p in products on pc.ProductId equals p.ProductId
                         join cat in categories on p.CategoryId equals cat.CategoryId
                         select new JoinTables
                         {
                             Product = p,
                             Category = cat,
                             ProductCustomer = pc,
                             Customer = c
                         };
            return View(Result);
        }
        public IActionResult Search()
        {
            var result = _context.SubscriptionPlans.Include(x => x.Trainer).Include(x => x.Payments).ToList();
            return View(result);
        }
        [HttpPost]
        public IActionResult Search(DateTime? dateFrom, DateTime? dateTo)
        {
            var result = _context.ProductCustomers.Include(x => x.Product).Include(x => x.Customer).ToList();
            ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
            ViewBag.Price = result.Sum(x => x.Product.Price * x.Quantity);
            if (dateFrom == null && dateTo == null)
            {
                return View(result);
            }
            else if (dateFrom != null && dateTo == null)
            {
                result = result.Where(x => x.DateFrom.Value.Date >= dateFrom).ToList();
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.Price = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
            else if (dateFrom == null && dateTo != null)
            {
                result = result.Where(x => x.DateFrom.Value.Date <= dateTo).ToList();
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.Price = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
            else
            {
                result = result.Where(x => x.DateFrom.Value.Date <= dateTo && x.DateFrom.Value.Date >= dateFrom).ToList();
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.Price = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
        }
    }

}
