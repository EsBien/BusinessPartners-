using System;
using System.Collections.Generic;

namespace MyFirstWebProject.Models1;

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
