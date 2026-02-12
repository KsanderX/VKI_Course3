using System;
using System.Collections.Generic;

namespace AI_Demo.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
