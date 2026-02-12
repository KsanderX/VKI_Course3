using System;
using System.Collections.Generic;

namespace Demodem.Models;

public partial class User
{
    public int Id { get; set; }

    public int? FkUserRole { get; set; }

    public string? Fio { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual Role? FkUserRoleNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
