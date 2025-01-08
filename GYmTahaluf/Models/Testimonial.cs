using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Testimonial
{
    public decimal Id { get; set; }

    public decimal UserId { get; set; }

    public decimal? Rate { get; set; }

    public string? Text { get; set; }

    public DateTime? RatedTime { get; set; }

    public bool? Isapproved { get; set; }

    public bool? Isadminreview { get; set; }

    public string? Priority { get; set; }

    public string? Image { get; set; }

    public decimal? TrainerId { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Trainer { get; set; }

    public virtual User User { get; set; } = null!;
}
