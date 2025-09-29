using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class RepairPart
{
    public int Id { get; set; }

    public string? RepairParts { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
