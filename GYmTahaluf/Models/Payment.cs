using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Payment
{
    public decimal Id { get; set; }

    public string? PaymentStatus { get; set; }

    public decimal Amount { get; set; }

    public decimal UserId { get; set; }

    public decimal PlanId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? TransactionRef { get; set; }

    public bool? IsUsedPromo { get; set; }

    public decimal? Promocode { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual SubscriptionPlan Plan { get; set; } = null!;

    public virtual Promocode? PromocodeNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
