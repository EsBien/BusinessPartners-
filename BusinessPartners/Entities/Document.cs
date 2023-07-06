using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
     public struct DocumentType
    {
        public const string SaleOrders = "SO";
        public const string PurchaseOrders ="PO";
    }
    public class Document: DocumentComponent
    {
       
       
        public string BPCode { get; set; }
        public string IteamCode { get; set; }
        public string Comment { get; set; }
      
  
        public decimal Quantity { get; set; }
    }
}
