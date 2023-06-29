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
      
        public string saleType { get; } = "SO"; //SaleOrders
        public string purchasType { get; } = "PO";//PurchaseOrders
        public string documentType { get; set; }
        public string BPCode { get; set; }
        public string IteamCode { get; set; }
        public int UserCode { get; set; }
        public string Comment { get; set; }


        //public List<PurchaseOrdersLine> Lines { get; set; }

        //public Document()
        //{
        //    Lines = new List<PurchaseOrdersLine>();
        //}

        //public void AddLine(PurchaseOrdersLine line)
        //{
        //    Lines.Add(line);
        //}
        public DateTime? CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public DateTime? CreateDateBy { get; set; }

        public DateTime? LastUpdateDateBy { get; set; }
    }
}
