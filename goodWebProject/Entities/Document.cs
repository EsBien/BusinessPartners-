using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Document
    {
        public enum DocumentType
        {
            SaleOrders,
            PurchaseOrders
        }
      
        public string saleTypeCode { get; } = "SO"; //SaleOrders
        public string purchasTypeCode { get; } = "PO";//PurchaseOrders
        public string documentType { get; set; }
        public string BPCode { get; set; }
        public string IteamCode { get; set; }
        public int UserCode { get; set; }
        public string Comment { get; set; }
        public DateTime? CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public DateTime? CreateDateBy { get; set; }

        public DateTime? LastUpdateDateBy { get; set; }

        public double Quantity { get; set; } =1;
    }
}
