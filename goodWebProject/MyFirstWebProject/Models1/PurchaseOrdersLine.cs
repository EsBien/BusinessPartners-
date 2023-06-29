﻿using System;
using System.Collections.Generic;

namespace MyFirstWebProject.Models1;

public partial class PurchaseOrdersLine
{
    public int LineId { get; set; }

    public int? DocId { get; set; }

    public string ItemCode { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? LastUpdatedBy { get; set; }

    public virtual UserTbl CreatedByNavigation { get; set; }

    public virtual PurchaseOrder Doc { get; set; }

    public virtual Item ItemCodeNavigation { get; set; }

    public virtual UserTbl LastUpdatedByNavigation { get; set; }
}
