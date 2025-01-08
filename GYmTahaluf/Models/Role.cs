using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
