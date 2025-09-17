using System;
using System.Collections.Generic;

namespace Decor.Models;

public partial class ProductMaterialsImport
{
    public int Id { get; set; }

    public int? FkПродукция { get; set; }

    public int? FkНаименованиеМатериала { get; set; }

    public double? НеобходимоеКоличествоМатериала { get; set; }

    public virtual MaterialsImport? FkНаименованиеМатериалаNavigation { get; set; }

    public virtual ProductsImport? FkПродукцияNavigation { get; set; }
}
