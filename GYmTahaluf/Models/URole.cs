using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class URole
{
    public decimal RoleId { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
