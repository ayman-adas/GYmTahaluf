using GYmTahaluf.Models;
using System.Numerics;

public class DashboardViewModel
{
    // Key statistics
    public int RegisteredMembersCount { get; set; }
    public int ActiveSubscriptionsCount { get; set; }
    public decimal TotalRevenue { get; set; }

    // Subscription Reports
    public List<MonthlyReport> MonthlySubscriptionReport { get; set; }
    public List<AnnualReport> AnnualSubscriptionReport { get; set; }

    // Data for top plans, tostominals, payments, and users
    public List<SubscriptionPlan> Item1 { get; set; } // Top Plans
    public List<Payment> Item2 { get; set; } // Payments
    public List<User> Item3 { get; set; } // Users
    public List<Testimonial> Item4 { get; set; } // Tostominals
}
