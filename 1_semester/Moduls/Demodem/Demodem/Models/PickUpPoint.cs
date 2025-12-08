using System;
using System.Collections.Generic;

namespace Demodem.Models;

public partial class PickUpPoint
{
    public int Id { get; set; }

    public string? Addreses { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
