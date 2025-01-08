using System;
using System.Collections.Generic;

namespace GYmTahaluf.Models;

public partial class Day
{
    public decimal Id { get; set; }

    public string DaysName { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
