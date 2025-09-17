using System;
using System.Collections.Generic;

namespace Decor.Models;

public partial class MaterialTypeImport
{
    public int Id { get; set; }

    public string? ТипМатериала { get; set; }

    public double? ПроцентБракаМатериала { get; set; }

    public virtual ICollection<MaterialsImport> MaterialsImports { get; set; } = new List<MaterialsImport>();
}
