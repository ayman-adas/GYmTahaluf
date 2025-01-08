using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Schedule
{
    public decimal Id { get; set; }

    public decimal UserId { get; set; }

    public decimal PlanId { get; set; }

    public decimal StartDay { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? Isavailable { get; set; }

    public virtual SubscriptionPlan Plan { get; set; } = null!;

    public virtual Day StartDayNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
