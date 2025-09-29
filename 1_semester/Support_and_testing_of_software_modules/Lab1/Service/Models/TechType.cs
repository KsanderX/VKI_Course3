using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class TechType
{
    public int Id { get; set; }

    public string? HomeTechType { get; set; }

    public virtual ICollection<TechModel> TechModels { get; set; } = new List<TechModel>();
}
