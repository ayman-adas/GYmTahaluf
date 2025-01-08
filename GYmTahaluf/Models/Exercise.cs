using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Exercise
{
    public decimal Id { get; set; }

    public decimal PlanId { get; set; }

    public string Title { get; set; } = null!;

    public decimal? Repetition { get; set; }

    public string? Description { get; set; }

    public decimal? RestPeriod { get; set; }

    public string? Difficulty { get; set; }

    public virtual SubscriptionPlan Plan { get; set; } = null!;
}
