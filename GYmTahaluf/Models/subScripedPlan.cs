using GYmTahaluf.Models;

public class SubScripedPlan
{
    public SubscriptionPlan Plan { get; set; }
    public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    public Schedule Schedule { get; set; } = new Schedule();
}
