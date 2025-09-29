using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class TechModel
{
    public string? HomeTechModel { get; set; }

    public int Id { get; set; }

    public int? FkTechType { get; set; }

    public virtual TechType? FkTechTypeNavigation { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
