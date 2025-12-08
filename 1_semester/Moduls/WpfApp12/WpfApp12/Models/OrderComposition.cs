using System;
using System.Collections.Generic;

namespace WpfApp12.Models;

public partial class OrderComposition
{
    public int Id { get; set; }

    public int? PruductId { get; set; }

    public double? Quantity { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Pruduct { get; set; }
}
