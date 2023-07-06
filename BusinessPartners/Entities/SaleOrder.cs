using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;


public partial class SaleOrder : TrackableEntity
{
    public int Id { get; set; }

    public string Bpcode { get; set; }

    //public DateTime? CreateDate { get; set; }

    //public DateTime? LastUpdateDate { get; set; }

    //public int? CreatedBy { get; set; }

    //public int? LastUpdatedBy { get; set; }

    public virtual Bp BpcodeNavigation { get; set; }

    public virtual UserTbl CreatedByNavigation { get; set; }

    public virtual UserTbl LastUpdatedByNavigation { get; set; }

    public virtual ICollection<SaleOrdersLine> SaleOrdersLines { get; set; } = new List<SaleOrdersLine>();

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
