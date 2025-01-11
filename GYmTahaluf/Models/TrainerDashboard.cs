using GYmTahaluf.Models;

public class TrainerDashboard
{
    public List<SubscriptionPlan> Plans { get; set; } // List of subscription plans for the trainer
    public List<User> Members { get; set; } // List of schedules (members) associated with the trainer
    public decimal TotalPayment { get; set; } // Total payment amount for the trainer
}