using System;
using System.Collections.Generic;

namespace Demodemo.Models;

public partial class OrderContent
{
    public int Id { get; set; }

    public int? FkProduct { get; set; }

    public double? Quantity { get; set; }

    public int? FkOrder { get; set; }

    public virtual Order? FkOrderNavigation { get; set; }

    public virtual Product? FkProductNavigation { get; set; }
}
