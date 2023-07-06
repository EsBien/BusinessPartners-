using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;


public partial class SaleOrdersLine : TrackableEntity
{
    public int LineId { get; set; }

    public int? DocId { get; set; }

    public string ItemCode { get; set; }

    public decimal? Quantity { get; set; }


    public virtual UserTbl CreatedByNavigation { get; set; }

    public virtual SaleOrder Doc { get; set; }

    public virtual Item ItemCodeNavigation { get; set; }

    public virtual UserTbl LastUpdatedByNavigation { get; set; }

    public virtual ICollection<SaleOrdersLinesComment> SaleOrdersLinesComments { get; set; } = new List<SaleOrdersLinesComment>();

    public override void setCreateDate(DateTime? d)
    {
        if (!isCreateDateModified)
        {
            CreateDate = DateTime.Now;
            isCreateDateModified = true;
        }
    }

    public override void setCreatedBy(int? id)
    {
        if (!isCreatedByModified)
        {
            CreatedBy = id;
            isCreatedByModified = true;
        }
    }
}
