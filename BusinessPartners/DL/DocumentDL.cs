using DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        BusinessPartnersContext _context;
        public DocumentDL(BusinessPartnersContext businessPartnersContext) { 
                _context = businessPartnersContext;
        }

        private async Task<bool> checkDocument(Document d , string bpTpe)
        {
            Bp getBP = await _context.Bps.FindAsync(d.BPCode);
            Item item =await _context.Items.FindAsync(d.IteamCode);
            if (getBP.Active != true || getBP.Bptype == bpTpe || item.Active != true || d.Quantity < 0)
            {
                return false;
            }
            return true;
        }
        public async Task<Document> PostPurchasOders(Document d)
        {
            if (checkDocument(d,"S").Result)
            {
                return null;
            }
            PurchaseOrder purchaseOrder = new PurchaseOrder
            {
                Bpcode = d.BPCode,
                LastUpdateDate = null,
                LastUpdatedBy = null
            };
            purchaseOrder.setCreatedBy(d.UserCode);
            purchaseOrder.setCreateDate(DateTime.Now);
            PurchaseOrdersLine purchaseOrdersLine = new PurchaseOrdersLine
            {

                Quantity = d.Quantity,
                LastUpdateDate = null,
                LastUpdatedBy = null
            };
            purchaseOrdersLine.setCreatedBy(d.UserCode);
            purchaseOrdersLine.setCreateDate(DateTime.Now);
            purchaseOrder.PurchaseOrdersLines.Add(purchaseOrdersLine);
   
            d.setCreateDate(purchaseOrder.CreateDate);
            d.setCreateDate(purchaseOrder.CreateDate);
            d.LastUpdateDate = null;
            d.LastUpdatedBy = null;
            await _context.PurchaseOrders.AddAsync(purchaseOrder);
            await _context.SaveChangesAsync();
            return d;
        }

        private SaleOrder CreateSaleOder(Document d)
        {
            SaleOrder saleOrder = new SaleOrder
            {
                Bpcode = d.BPCode,
                LastUpdateDate = null,
                LastUpdatedBy = null
            };
            saleOrder.setCreateDate(DateTime.Now);
            saleOrder.setCreatedBy(d.UserCode);
            return saleOrder;
        }
        private  SaleOrdersLine CreateSaleOderLines(Document d)
        {

            SaleOrdersLine saleOrdersLine = new SaleOrdersLine
            {
                //DocId = saleOrderId,
                Quantity = d.Quantity,
                ItemCode = d.IteamCode,
                LastUpdateDate = null,
                LastUpdatedBy = null
            };
            saleOrdersLine.setCreatedBy(d.UserCode);
            saleOrdersLine.setCreateDate(DateTime.Now);
            return saleOrdersLine;
        }
      

        public async Task<Document> PostSaleOders(Document d)
        {
            if (!checkDocument(d,"V").Result)
            {
                return null;
            }
          
            SaleOrder saleOrder = CreateSaleOder(d);

            SaleOrdersLine saleOrdersLine =CreateSaleOderLines(d);
            saleOrder.SaleOrdersLines.Add(saleOrdersLine);
            
            SaleOrdersLinesComment saleOrdersLineComment = new SaleOrdersLinesComment() {Comment=d.Comment };
            saleOrdersLine.SaleOrdersLinesComments.Add(saleOrdersLineComment);
            saleOrder.SaleOrdersLinesComments.Add(saleOrdersLineComment);
            await _context.SaleOrders.AddAsync(saleOrder);
            await _context.SaveChangesAsync();
            d.setCreateDate(saleOrder.CreateDate);
            d.setCreatedBy(saleOrder.CreatedBy);
            d.LastUpdateDate = null;
            d.LastUpdatedBy = null;
            return d;
        }

        //        • It’s not possible to update a document that doesn’t exists
        //• It’s not possible to change the document type
        //• It’s not possible to use a business partner of type V in a sale document
        //• It’s not possible to use a business partner of type S in a purchase document
        //• It’s not possible to change the business partner to an inactive one
        //• It’s not possible to delete all the document lines
        public async Task<Document> UpdateDocumentSaleOders(Document d)
        {
            SaleOrder saleOrder = _context.SaleOrders
            .Include(so => so.SaleOrdersLines)
            .Include(so => so.SaleOrdersLinesComments)
            .FirstOrDefault(s => s.Id == d.ID);
            if (saleOrder == null)
            {
                return null;
            }
            Bp bp = _context.Bps.FirstOrDefault(b => b.Bpcode == d.BPCode);
            Item item = _context.Items.FindAsync(d.IteamCode).Result;
            if (bp != null && bp.Bptype =="V" || item.Active == false)
                return null;
           
            saleOrder.LastUpdateDate = DateTime.Now;
            saleOrder.Bpcode = d.BPCode;
            saleOrder.LastUpdatedBy = d.UserCode;

            foreach (var s in saleOrder.SaleOrdersLines)
            { 
                s.Quantity = d.Quantity;
                s.ItemCode = d.IteamCode;
                s.LastUpdateDate = DateTime.Now;
                s.LastUpdatedBy = d.UserCode;
                s.Quantity= d.Quantity;
            }

            foreach (var s in saleOrder.SaleOrdersLinesComments)
            {
                s.Comment = d.Comment;
            }

            await _context.SaveChangesAsync();
            d.LastUpdateDate=saleOrder.LastUpdateDate;
            d.LastUpdatedBy = saleOrder.LastUpdatedBy;

            return d;
        }

        public async Task<Document> UpdateDocumentPurchasOders(Document d)
        {
            PurchaseOrder purchaseOrder =await _context.PurchaseOrders
                   .Include(so => so.PurchaseOrdersLines)
                .FirstOrDefaultAsync(s => s.Id == d.ID);
            if (purchaseOrder == null)
            {
                return null;
            }
            Bp bp =await _context.Bps.FirstOrDefaultAsync(b => b.Bpcode == d.BPCode);
            if (bp != null && bp.Bptype == "S")
                return null;
            purchaseOrder.LastUpdateDate = DateTime.Now;
            purchaseOrder.Bpcode = d.BPCode;
            purchaseOrder.LastUpdatedBy = d.UserCode;
       
            foreach(var p in purchaseOrder.PurchaseOrdersLines)
            {  
                p.Quantity = d.Quantity;
                p.ItemCode = d.IteamCode;
                p.LastUpdateDate = DateTime.Now;
                p.LastUpdatedBy = d.ID;
            }
            await _context.SaveChangesAsync();

            return d;
        }

        public async Task DeleteSaleOders(int id)
        {
            SaleOrder saleOrder = _context.SaleOrders.FirstOrDefaultAsync(s => s.Id == id).Result;
            if (saleOrder == null)
                return;
            var saleOrdersLine = _context.SaleOrdersLines.Where(s => s.DocId == saleOrder.Id).ToList();
            foreach( var s in saleOrdersLine)
            {
                SaleOrdersLinesComment comment = _context.SaleOrdersLinesComments.FirstOrDefault(a => a.LineId == s.LineId && s.DocId == a.DocId);
                if(comment!=null)
                {
                    _context.SaleOrdersLinesComments.Remove(comment);
                    await _context.SaveChangesAsync();
                }
            }
            saleOrdersLine.ForEach(s => _context.SaleOrdersLines.Remove(s));
            await _context.SaveChangesAsync();
             _context.SaleOrders.Remove(saleOrder);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePurchasOders(int id)
        {
            PurchaseOrder purchaseOrder = _context.PurchaseOrders.FirstOrDefaultAsync(s => s.Id == id).Result;
            if (purchaseOrder == null)
                return;
            var purchasOderLines = _context.PurchaseOrdersLines.Where(s => s.DocId == purchaseOrder.Id).ToList();
           
            purchasOderLines.ForEach(s => _context.PurchaseOrdersLines.Remove(s));
            await _context.SaveChangesAsync();
            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<DocumentDTO> GetDocumentSaleOder(int id)
        {
            SaleOrder saleOrder = _context.SaleOrders.FirstOrDefault(s => s.Id == id);
            if (saleOrder == null)
                return null;
            SaleOrdersLine saleOrdersLine = _context.SaleOrdersLines.FirstOrDefault(s => s.DocId == saleOrder.Id);
            if (saleOrdersLine == null)
                return null;
            SaleOrdersLinesComment saleOrdersLinesComment = _context.SaleOrdersLinesComments.FirstOrDefault(s => s.DocId == saleOrder.Id && s.LineId == saleOrdersLine.LineId);
            UserTbl user = _context.UserTbls.FirstOrDefault(u => u.Id == saleOrder.CreatedBy);
            Bp bp = _context.Bps.FirstOrDefault(b => b.Bpcode == saleOrder.Bpcode);
            Item item = await _context.Items.FirstOrDefaultAsync(o => o.ItemCode == saleOrdersLine.ItemCode);
            DocumentDTO documentDTO = new DocumentDTO()
            {
                ID = id,
                BPCode = saleOrder.Bpcode,
                BPName = bp != null ? bp.Bpname : "",
                userFullName = user!=null ? user.FullName :"",
                itemName = item != null ? item.ItemName :"",
                isActivItem = item != null ? item.Active : false,
                IteamCode = item != null ? item.ItemCode :"",
                UserCode = user.Id,
                Comment = saleOrdersLinesComment != null ? saleOrdersLinesComment.Comment :"",
                LastUpdateDate = saleOrder.LastUpdateDate,
                LastUpdatedBy = saleOrder.LastUpdatedBy,
                Quantity= saleOrdersLine.Quantity
            };
            documentDTO.setCreateDate(saleOrder.CreateDate);
            documentDTO.setCreatedBy(saleOrder.CreatedBy);
            return documentDTO;
        }

        public async Task<DocumentDTO> GetDocumentPurchasOder(int id)
        {

            PurchaseOrder purchasOrder = _context.PurchaseOrders.FirstOrDefault(s => s.Id == id);
            if (purchasOrder == null)
                return null;
            PurchaseOrdersLine purchasOrdersLine = _context.PurchaseOrdersLines.FirstOrDefault(s => s.DocId == purchasOrder.Id);
            if (purchasOrdersLine == null)
                return null;
            UserTbl user =await _context.UserTbls.FirstOrDefaultAsync(u => u.Id == purchasOrder.CreatedBy);
            Bp bp = await _context.Bps.FirstOrDefaultAsync(b => b.Bpcode == purchasOrder.Bpcode);
            Item item = await _context.Items.FirstOrDefaultAsync(o => o.ItemCode == purchasOrdersLine.ItemCode);
            DocumentDTO documentDTO = new DocumentDTO()
            {
                ID = id,
                BPCode = purchasOrder.Bpcode,
                BPName = bp != null ? bp.Bpname : "",
                userFullName = user != null ? user.FullName : "",
                itemName = item != null ? item.ItemName : "",
                isActivItem = item != null ? item.Active : false,
                IteamCode = item != null ? item.ItemCode : "",
                UserCode = user.Id,
                LastUpdateDate = purchasOrder.LastUpdateDate,
                LastUpdatedBy = purchasOrder.LastUpdatedBy,
                Quantity = purchasOrdersLine.Quantity
            };
            documentDTO.setCreateDate(purchasOrder.CreateDate);
            documentDTO.setCreatedBy(purchasOrder.CreatedBy);
            return documentDTO;
        }
    }
    
}
