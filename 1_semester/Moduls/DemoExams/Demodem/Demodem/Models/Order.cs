using System;
using System.Collections.Generic;

namespace Demodem.Models;

public partial class Order
{
    public int Id { get; set; }

    public double? OrderNumber { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDay { get; set; }

    public int? FkPickUpPoint { get; set; }

    public int? FkClient { get; set; }

    public double? CodeToReceive { get; set; }

    public int? FkOrderStatus { get; set; }

    public virtual User? FkClientNavigation { get; set; }

    public virtual OrderStatus? FkOrderStatusNavigation { get; set; }

    public virtual PickUpPoint? FkPickUpPointNavigation { get; set; }

    public virtual ICollection<OrderContent> OrderContents { get; set; } = new List<OrderContent>();
}
