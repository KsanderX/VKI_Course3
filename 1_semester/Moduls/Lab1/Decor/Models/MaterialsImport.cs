using System;
using System.Collections.Generic;

namespace Decor.Models;

public partial class MaterialsImport
{
    public int Id { get; set; }

    public string? НаименованиеМатериала { get; set; }

    public int? FkТипМатериала { get; set; }

    public double? ЦенаЕдиницыМатериала { get; set; }

    public double? КоличествоНаСкладе { get; set; }

    public double? МинимальноеКоличество { get; set; }

    public double? КоличествоВУпаковке { get; set; }

    public string? ЕдиницаИзмерения { get; set; }

    public virtual MaterialTypeImport? FkТипМатериалаNavigation { get; set; }

    public virtual ICollection<ProductMaterialsImport> ProductMaterialsImports { get; set; } = new List<ProductMaterialsImport>();
}
