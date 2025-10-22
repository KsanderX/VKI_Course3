using System;
using System.Collections.Generic;

namespace Lab2_Module2.Models;

public partial class Order
{
    public int Id { get; set; }

    public double? AmountOfRent { get; set; }

    public DateTime? DateRent { get; set; }

    public int? FkAddressPickUpPoint { get; set; }

    public int? FkUser { get; set; }

    public double? CodeToReceive { get; set; }

    public int? FkOrderStatus { get; set; }

    public virtual PickUpPoint? FkAddressPickUpPointNavigation { get; set; }

    public virtual OrderStatus? FkOrderStatusNavigation { get; set; }

    public virtual User? FkUserNavigation { get; set; }

    public virtual ICollection<OrderEquipment> OrderEquipments { get; set; } = new List<OrderEquipment>();
}
