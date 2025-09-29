using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class RequestStatus
{
    public int Id { get; set; }

    public string? RequestStatus1 { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
