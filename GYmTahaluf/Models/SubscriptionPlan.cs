using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class SubscriptionPlan
{
    public decimal Id { get; set; }

    public decimal? TrainerId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Price { get; set; }

    public bool? Isvisible { get; set; }

    public string? Descriptions { get; set; }

    public string Title { get; set; } = null!;

    public string? Goal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public decimal DaysInWeek { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User? Trainer { get; set; }
}
