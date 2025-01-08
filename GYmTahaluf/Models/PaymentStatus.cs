using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class PaymentStatus
{
    public decimal Id { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }
}
