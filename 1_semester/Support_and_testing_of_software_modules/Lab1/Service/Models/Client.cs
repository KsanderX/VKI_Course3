using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Client
{
    public int UserId { get; set; }

    public string? Fio { get; set; }

    public string? Phone { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
