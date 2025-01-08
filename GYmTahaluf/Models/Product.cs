using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Product
{
    public decimal ProductId { get; set; }

    public string? Namee { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Price { get; set; }

    public decimal? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductCustomer> ProductCustomers { get; set; } = new List<ProductCustomer>();
}
