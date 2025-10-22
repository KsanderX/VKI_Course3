using System;
using System.Collections.Generic;

namespace Lab2_Module2.Models;

public partial class PickUpPoint
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
