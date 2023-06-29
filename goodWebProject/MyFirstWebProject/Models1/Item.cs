using System;
using System.Collections.Generic;

namespace MyFirstWebProject.Models1;

public partial class Item
{
    public string ItemCode { get; set; }

    public string ItemName { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLines { get; set; } = new List<PurchaseOrdersLine>();

    public virtual ICollection<SaleOrdersLine> SaleOrdersLines { get; set; } = new List<SaleOrdersLine>();
}
