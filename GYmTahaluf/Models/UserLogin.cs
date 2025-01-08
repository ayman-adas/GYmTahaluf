using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class UserLogin
{
    public decimal UserLoginId { get; set; }

    public string? UserName { get; set; }

    public string? Passwordd { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual URole? Role { get; set; }
}
