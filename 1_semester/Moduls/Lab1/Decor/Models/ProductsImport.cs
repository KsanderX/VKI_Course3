using System;
using System.Collections.Generic;

namespace Decor.Models;

public partial class ProductsImport
{
    public int Id { get; set; }

    public int? FkТипПродукции { get; set; }

    public string? НаименованиеПродукции { get; set; }

    public double? Артикул { get; set; }

    public double? МинимальнаяСтоимостьДляПартнера { get; set; }

    public double? ШиринаРулонаМ { get; set; }

    public virtual ProductTypeImport? FkТипПродукцииNavigation { get; set; }

    public virtual ICollection<ProductMaterialsImport> ProductMaterialsImports { get; set; } = new List<ProductMaterialsImport>();
}
