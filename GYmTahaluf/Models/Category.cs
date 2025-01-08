using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Category
{
    public decimal CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
