using System;
using System.Collections.Generic;

namespace Lab2_Module2.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public string? EquipmentName { get; set; }

    public string? RentalUnit { get; set; }

    public double? Price { get; set; }

    public int? FkSupplier { get; set; }

    public int? FkManufacturer { get; set; }

    public int? FkEquipmentType { get; set; }

    public double? Discount { get; set; }

    public double? NumberUnits { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual EquipmentType? FkEquipmentTypeNavigation { get; set; }

    public virtual Manufacturer? FkManufacturerNavigation { get; set; }

    public virtual Supplier? FkSupplierNavigation { get; set; }

    public virtual ICollection<OrderEquipment> OrderEquipments { get; set; } = new List<OrderEquipment>();
}
