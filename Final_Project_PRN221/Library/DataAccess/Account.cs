using System;
using System.Collections.Generic;

namespace Library.DataAccess;

public partial class Account
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Role { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
