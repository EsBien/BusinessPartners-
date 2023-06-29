using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * It’s not possible to add a document for a business partner that is not active
• It’s not possible to add a sale document for a business partner of type V
• It’s not possible to add a purchase document for a business partner of type S
• It’s not possible to add a document without lines
• It’s not possible to sale/purchase an item that is not active
 */
namespace DL
{
    public class DocumentDL : IDocumentDL
    {
        BusinessPartnersContext _contextl;
        public DocumentDL(BusinessPartnersContext businessPartnersContext) { 
                _contextl = businessPartnersContext;
        }

        public async Task<Document> PostPurchasOders(Document d)
        {

            Bp getBP =  _contextl.Bps.FindAsync(d.BPCode).Result;
            Item item = _contextl.Items.FindAsync(d.IteamCode).Result;
            if (getBP.Active != true || getBP.Bptype== "S" ||item.Active !=true)
            {
                return null;
            }
    
            ////var userId = HttpContext.Session.GetInt32("UserId");
            PurchaseOrder purchaseOrder = new PurchaseOrder
            {
                Bpcode = d.BPCode,
                CreateDate = DateTime.Now,
                LastUpdateDate = null,
                CreatedBy = d.UserCode,
                LastUpdatedBy = null
            };
            await _contextl.PurchaseOrders.AddAsync(purchaseOrder);
            await _contextl.SaveChangesAsync();
            d.CreateDate= DateTime.Now;
            d.LastUpdateDate = null;
            d.LastUpdateDateBy = null;
            return d;
        }

        public Task<Document> PostSaleOders(Document d)
        {
            throw new NotImplementedException();
        }



    }
}
