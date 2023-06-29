using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;

public partial class Bp
{
    public string Bpcode { get; set; }

    public string Bpname { get; set; }

    public string Bptype { get; set; }

    public bool? Active { get; set; }

    public virtual Bptype BptypeNavigation { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
