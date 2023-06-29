using System;
using System.Collections.Generic;

namespace MyFirstWebProject.Models1;

public partial class UserTbl
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrderCreatedByNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderLastUpdatedByNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLineCreatedByNavigations { get; set; } = new List<PurchaseOrdersLine>();

    public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLineLastUpdatedByNavigations { get; set; } = new List<PurchaseOrdersLine>();

    public virtual ICollection<SaleOrder> SaleOrderCreatedByNavigations { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderLastUpdatedByNavigations { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrdersLine> SaleOrdersLineCreatedByNavigations { get; set; } = new List<SaleOrdersLine>();

    public virtual ICollection<SaleOrdersLine> SaleOrdersLineLastUpdatedByNavigations { get; set; } = new List<SaleOrdersLine>();
}
