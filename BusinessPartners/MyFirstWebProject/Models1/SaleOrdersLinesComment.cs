using System;
using System.Collections.Generic;

namespace MyFirstWebProject.Models1;

public partial class SaleOrdersLinesComment
{
    public int CommentLineId { get; set; }

    public int? DocId { get; set; }

    public int? LineId { get; set; }

    public string Comment { get; set; }

    public virtual SaleOrder Doc { get; set; }

    public virtual SaleOrdersLine Line { get; set; }
}
