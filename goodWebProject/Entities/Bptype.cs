using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;


public partial class Bptype
{
    public string TypeCode { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<Bp> Bps { get; set; } = new List<Bp>();
}
