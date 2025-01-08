using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Invoice
{
    public decimal Id { get; set; }

    public decimal PaymentId { get; set; }

    public DateTime? GeneratedAt { get; set; }

    public string Url { get; set; } = null!;

    public string? Status { get; set; }

    public string? InvoiceNumber { get; set; }

    public virtual Payment Payment { get; set; } = null!;
}
