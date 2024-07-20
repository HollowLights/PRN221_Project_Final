using System;
using System.Collections.Generic;

namespace Library.DataAccess;

public partial class Table
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public bool? IsOn { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Type Type { get; set; } = null!;
}
