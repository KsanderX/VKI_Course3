using System;
using System.Collections.Generic;

namespace Demodem.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public int? FkProductName { get; set; }

    public int? FkUnit { get; set; }

    public double? Price { get; set; }

    public int? FkSupplier { get; set; }

    public int? FkManufacturer { get; set; }

    public int? FkProductCategory { get; set; }

    public double? Discount { get; set; }

    public double? CountOnStorage { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual Manufacturer? FkManufacturerNavigation { get; set; }

    public virtual ProductCategory? FkProductCategoryNavigation { get; set; }

    public virtual ProductName? FkProductNameNavigation { get; set; }

    public virtual Supplier? FkSupplierNavigation { get; set; }

    public virtual Unit? FkUnitNavigation { get; set; }

    public virtual ICollection<OrderContent> OrderContents { get; set; } = new List<OrderContent>();
}
