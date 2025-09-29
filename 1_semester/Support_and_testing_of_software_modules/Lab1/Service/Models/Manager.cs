using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Manager
{
    public int UserId { get; set; }

    public string? Fio { get; set; }

    public string? Phone { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Type { get; set; }
}
