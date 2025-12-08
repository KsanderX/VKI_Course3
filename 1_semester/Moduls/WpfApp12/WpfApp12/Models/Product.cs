using System;
using System.Collections.Generic;

namespace WpfApp12.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public int? ProductNameId { get; set; }

    public int? UnitId { get; set; }

    public double? Price { get; set; }

    public int? SupplierId { get; set; }

    public int? ProviderId { get; set; }

    public int? ProductCategoryId { get; set; }

    public double? Discount { get; set; }

    public double? Quantity { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<OrderComposition> OrderCompositions { get; set; } = new List<OrderComposition>();

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ProductName? ProductName { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual Unit? Unit { get; set; }
}
