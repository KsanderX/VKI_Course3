using System;
using System.Collections.Generic;

namespace Demodemo.Models;

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


//Scaffold-DbContext "Server=server;DataBase=DB;Trust Server Certificate=True; Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models