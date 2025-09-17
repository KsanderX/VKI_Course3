using System;
using System.Collections.Generic;

namespace Decor.Models;

public partial class ProductTypeImport
{
    public int Id { get; set; }

    public string? ТипПродукции { get; set; }

    public double? КоэффициентТипаПродукции { get; set; }

    public virtual ICollection<ProductsImport> ProductsImports { get; set; } = new List<ProductsImport>();
}
