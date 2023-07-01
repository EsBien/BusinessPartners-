using System;
using System.Collections.Generic;

namespace MyFirstWebProject.Models1;

public partial class Bptype
{
    public string TypeCode { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<Bp> Bps { get; set; } = new List<Bp>();
}
