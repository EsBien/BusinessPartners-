using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;


public partial class Item
{
    public string ItemCode { get; set; }

    public string ItemName { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLines { get; set; } = new List<PurchaseOrdersLine>();

    public virtual ICollection<SaleOrdersLine> SaleOrdersLines { get; set; } = new List<SaleOrdersLine>();
}
