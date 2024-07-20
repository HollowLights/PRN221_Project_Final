using System;
using System.Collections.Generic;

namespace Library.DataAccess;

public partial class Order
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public double? Discount { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int ?OrderBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Table Table { get; set; } = null!;

}
