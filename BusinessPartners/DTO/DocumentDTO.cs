using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO
{
    public class DocumentDTO :DocumentComponent
    {
       
        public string BPCode { get; set; }
        public string BPName { get; set; } = string.Empty;
        public string userFullName { get; set; }
        
        public string itemName { get; set; }
        public bool? isActivItem { get; set; }
        public string IteamCode { get; set; }
        public string Comment { get; set; }
        
        public DateTime? CreateDate { get; set; }
     

        public decimal? Quantity { get; set; }
    }
}
