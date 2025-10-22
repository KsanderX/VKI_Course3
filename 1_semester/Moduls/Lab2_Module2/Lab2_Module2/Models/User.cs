using System;
using System.Collections.Generic;

namespace Lab2_Module2.Models;

public partial class User
{
    public int Id { get; set; }

    public int? FkRole { get; set; }

    public string? Fio { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual Role? FkRoleNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
