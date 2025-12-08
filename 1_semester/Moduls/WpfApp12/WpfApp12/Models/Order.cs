using System;
using System.Collections.Generic;

namespace WpfApp12.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? PointId { get; set; }

    public int? UserId { get; set; }

    public double? Code { get; set; }

    public int? OrderStatusId { get; set; }

    public virtual ICollection<OrderComposition> OrderCompositions { get; set; } = new List<OrderComposition>();

    public virtual OrderStatus? OrderStatus { get; set; }

    public virtual Point? Point { get; set; }

    public virtual User? User { get; set; }
}
