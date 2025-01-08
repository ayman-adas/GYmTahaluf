using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Page
{
    public decimal Id { get; set; }

    public string Title { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Content { get; set; }

    public byte[]? Image { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
