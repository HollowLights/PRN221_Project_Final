using System;
using System.Collections.Generic;

namespace Library.DataAccess;

public partial class Type
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal PricePerHour { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
