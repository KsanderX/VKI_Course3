using System;
using System.Collections.Generic;

namespace Lab2_Module2.Models;

public partial class OrderEquipment
{
    public int Id { get; set; }

    public int? FkEquipment { get; set; }

    public int? FkOrder { get; set; }

    public double? Quantity { get; set; }

    public virtual Product? FkEquipmentNavigation { get; set; }

    public virtual Order? FkOrderNavigation { get; set; }
}
