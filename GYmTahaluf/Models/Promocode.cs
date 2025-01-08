using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Promocode
{
    public decimal Id { get; set; }

    public string Code { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public decimal? Percent { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Isvisible { get; set; }

    public decimal? MinAmount { get; set; }

    public decimal? MaxUses { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
