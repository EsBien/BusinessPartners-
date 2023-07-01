using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;


public partial class PurchaseOrder
{
    public int Id { get; set; }

    public string Bpcode { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? LastUpdatedBy { get; set; }

    public virtual Bp BpcodeNavigation { get; set; }

    public virtual UserTbl CreatedByNavigation { get; set; }

    public virtual UserTbl LastUpdatedByNavigation { get; set; }

    public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLines { get; set; } = new List<PurchaseOrdersLine>();
}
