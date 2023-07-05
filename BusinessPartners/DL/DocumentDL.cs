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

        private bool checkDocument(Document d , string bpTpe)
        {
            Bp getBP = _context.Bps.FindAsync(d.BPCode).Result;
            Item item = _context.Items.FindAsync(d.IteamCode).Result;
            if (getBP.Active != true || getBP.Bptype == bpTpe || item.Active != true || d.Quantity > 0)
            {
                return false;
            }
            return true;
        }
        public async Task<Document> PostPurchasOders(Document d)
        {
            if (checkDocument(d,"S"))
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
            await _context.PurchaseOrders.AddAsync(purchaseOrder);
            await _context.SaveChangesAsync();
            var purchaseOrderId = _context.PurchaseOrders.FirstOrDefaultAsync(p => p.Bpcode== purchaseOrder.Bpcode &&
            p.CreateDate==purchaseOrder.CreateDate && p.CreatedBy == purchaseOrder.CreatedBy).Result.Id;
            PurchaseOrdersLine purchaseOrdersLine = new PurchaseOrdersLine
            {
                DocId = purchaseOrderId,
                Quantity = d.Quantity,
                CreateDate = DateTime.Now,
                LastUpdateDate = null,
                CreatedBy = d.UserCode,
                LastUpdatedBy = null
            };
            await _context.PurchaseOrdersLines.AddAsync(purchaseOrdersLine);
            await _context.SaveChangesAsync();
            d.setCreateDate(purchaseOrder.CreateDate);
            d.setCreateDate(purchaseOrder.CreateDate);
            d.LastUpdateDate = null;
            d.LastUpdateDateBy = null;
            return d;
        }

        private async Task<SaleOrder> addSaleOderToDB(Document d)
        {
            SaleOrder saleOrder = new SaleOrder
            {
                Bpcode = d.BPCode,
                CreateDate = DateTime.Now,
                LastUpdateDate = null,
                CreatedBy = d.UserCode,
                LastUpdatedBy = null
            };
            await _context.SaleOrders.AddAsync(saleOrder);
            await _context.SaveChangesAsync();
            return saleOrder;
        }
        private async Task<SaleOrdersLine> addSaleOderLinesToDB(Document d,int saleOrderId)
        {

            SaleOrdersLine saleOrdersLine = new SaleOrdersLine
            {
                DocId = saleOrderId,
                Quantity = d.Quantity,
                ItemCode = d.IteamCode,
                CreateDate = DateTime.Now,
                LastUpdateDate = null,
                CreatedBy = d.UserCode,
                LastUpdatedBy = null
            };
            await _context.SaleOrdersLines.AddAsync(saleOrdersLine);
            await _context.SaveChangesAsync();
            return saleOrdersLine;
        }
        private async Task<SaleOrdersLinesComment> addSaleOderLinesCommensToDB(Document d, int saleOrderId,int saleOrdersLineId)
        {

            SaleOrdersLinesComment saleOrdersLineComment = new SaleOrdersLinesComment
            {
                DocId = saleOrderId,
                LineId = saleOrderId,
                Comment = d.Comment
            };
            await _context.SaleOrdersLinesComments.AddAsync(saleOrdersLineComment);
            await _context.SaveChangesAsync();
            return saleOrdersLineComment;
        }

        private async Task<PurchaseOrder> updatePurchasOders(Document d, PurchaseOrder purchaseOrder)
        {

            PurchaseOrder newPurchaseOrder = purchaseOrder;
            newPurchaseOrder.LastUpdateDate = DateTime.Now;
            newPurchaseOrder.Bpcode = d.BPCode;
            newPurchaseOrder.LastUpdatedBy = d.UserCode;

            _context.Entry(purchaseOrder).CurrentValues.SetValues(newPurchaseOrder);
            await _context.SaveChangesAsync();
            return newPurchaseOrder;
        }
        private async Task<PurchaseOrdersLine> updatePurchasOdersLinse(Document d, PurchaseOrdersLine currentPurchasOdersLine)
        {

            PurchaseOrdersLine purchaseOrdersLine = currentPurchasOdersLine;

            purchaseOrdersLine.Quantity = d.Quantity;
            purchaseOrdersLine.ItemCode = d.IteamCode;
            purchaseOrdersLine.LastUpdateDate = DateTime.Now;
            purchaseOrdersLine.CreatedBy = d.ID;
            purchaseOrdersLine.LastUpdatedBy = d.ID;
            _context.Entry(currentPurchasOdersLine).CurrentValues.SetValues(purchaseOrdersLine);
            await _context.SaveChangesAsync();
            return purchaseOrdersLine;
        }
        
        private async Task<SaleOrder> updateSaleOder(Document d,SaleOrder saleOrder)
        {

            SaleOrder newSaleOrder = saleOrder;
            newSaleOrder.LastUpdateDate = DateTime.Now;
            newSaleOrder.Bpcode = d.BPCode;
            newSaleOrder.LastUpdatedBy = d.UserCode;

            _context.Entry(saleOrder).CurrentValues.SetValues(newSaleOrder);
            await _context.SaveChangesAsync();
            return newSaleOrder;
        }
        
        private  SaleOrdersLine updateSaleOderLines(Document d, SaleOrdersLine currentSaleOdersLine)
        {
            SaleOrdersLine saleOrdersLine = currentSaleOdersLine;

            saleOrdersLine.Quantity = d.Quantity;
            saleOrdersLine.ItemCode = d.IteamCode;
            saleOrdersLine.LastUpdateDate = DateTime.Now;
            saleOrdersLine.LastUpdatedBy = d.UserCode;
            _context.Entry(currentSaleOdersLine).CurrentValues.SetValues(saleOrdersLine);
            return saleOrdersLine;

        }
        private SaleOrdersLinesComment updateSaleOderLinesComments(Document d, SaleOrdersLine currentSaleOdersLine)
        {
            var saleOrdersLinesComment = _context.SaleOrdersLinesComments.FirstOrDefault(x=>x.LineId== currentSaleOdersLine.LineId
            && x.DocId==currentSaleOdersLine.DocId);
            SaleOrdersLinesComment newSaleOrdersLinesComment = saleOrdersLinesComment;
            newSaleOrdersLinesComment.LineId = currentSaleOdersLine.LineId;
            newSaleOrdersLinesComment.Comment = d.Comment;
            newSaleOrdersLinesComment.DocId = currentSaleOdersLine.DocId;
            _context.Entry(saleOrdersLinesComment).CurrentValues.SetValues(newSaleOrdersLinesComment);

            return saleOrdersLinesComment;

        }
        public async Task<Document> PostSaleOders(Document d)
        {
            if (!checkDocument(d,"V"))
            {
                return null;
            }

            ////var userId = HttpContext.Session.GetInt32("UserId");
            SaleOrder saleOrder = await addSaleOderToDB(d);
           
            var saleOrderId = _context.SaleOrders.FirstOrDefaultAsync(p => p.Bpcode == saleOrder.Bpcode &&
            p.CreateDate == saleOrder.CreateDate && p.CreatedBy == saleOrder.CreatedBy).Result.Id;


            SaleOrdersLine saleOrdersLine =await addSaleOderLinesToDB(d,saleOrderId);

            var saleOderLineId = _context.SaleOrdersLines.FirstOrDefaultAsync(d => d.Doc == saleOrdersLine.Doc && d.CreateDate == saleOrdersLine.CreateDate);

            SaleOrdersLinesComment saleOrdersLineComment = await addSaleOderLinesCommensToDB(d, saleOrderId, saleOrdersLine.LineId);


            d.setCreateDate(saleOrder.CreateDate);
            d.setCreateDateBy(saleOrder.CreateDate);
            d.LastUpdateDate = null;
            d.LastUpdateDateBy = null;
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
            SaleOrder saleOrder= _context.SaleOrders.FirstOrDefault(s =>s.Id ==d.UserCode);
            if (saleOrder == null)
            {
                return null;
            }
            Bp bp = _context.Bps.FirstOrDefault(b => b.Bpcode == d.BPCode);
            Item item = _context.Items.FindAsync(d.IteamCode).Result;
            if (bp != null && bp.Bptype =="V" || item.Active == false)
                return null;
          
            SaleOrder newSaleOrder = await updateSaleOder(d,saleOrder);

            var saleOrderId = _context.SaleOrders.FirstOrDefaultAsync(p => p.Bpcode == saleOrder.Bpcode &&
         p.CreateDate == saleOrder.CreateDate && p.CreatedBy == saleOrder.CreatedBy).Result.Id;
            var currentSaleOdersLine = _context.SaleOrdersLines.FirstOrDefault(sl => sl.LineId ==
                _context.SaleOrdersLines.FirstOrDefault(s => s.DocId == saleOrderId).LineId);

            SaleOrdersLine saleOrdersLine =  updateSaleOderLines(d,currentSaleOdersLine);

            SaleOrdersLinesComment saleOrdersLinesComment = updateSaleOderLinesComments(d,saleOrdersLine);

            await _context.SaveChangesAsync();
            d.LastUpdateDate=saleOrder.LastUpdateDate;
            d.LastUpdateDateBy = saleOrder.LastUpdatedBy;

            return d;
        }

        public async Task<Document> UpdateDocumentPurchasOders(Document d)
        {
            PurchaseOrder purchaseOrder = _context.PurchaseOrders.FirstOrDefault(s => s.Id == d.ID);
            if (purchaseOrder == null)
            {
                return null;
            }
            Bp bp = _context.Bps.FirstOrDefault(b => b.Bpcode == d.BPCode);
            if (bp != null && bp.Bptype == "S")
                return null;

            PurchaseOrder newPurchaseOrder =await updatePurchasOders(d,purchaseOrder);
          
            var purchasOrderId = _context.PurchaseOrders.FirstOrDefaultAsync(p => p.Bpcode == purchaseOrder.Bpcode &&
         p.CreateDate == purchaseOrder.CreateDate && p.CreatedBy == purchaseOrder.CreatedBy).Result.Id;

            var currentPurchasOdersLine = _context.PurchaseOrdersLines.FirstOrDefault(sl => sl.LineId ==
                _context.PurchaseOrdersLines.FirstOrDefault(s => s.DocId == purchasOrderId).LineId);
            PurchaseOrdersLine purchaseOrdersLine =await updatePurchasOdersLinse(d, currentPurchasOdersLine);


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
                CreateDate = saleOrder.CreateDate,
                CreateDateBy = saleOrder.CreatedBy,
                LastUpdateDate = saleOrder.LastUpdateDate,
                LastUpdateDateBy = saleOrder.LastUpdatedBy,
                Quantity= saleOrdersLine.Quantity
            };
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
                CreateDate = purchasOrder.CreateDate,
                CreateDateBy = purchasOrder.CreatedBy,
                LastUpdateDate = purchasOrder.LastUpdateDate,
                LastUpdateDateBy = purchasOrder.LastUpdatedBy,
                Quantity = purchasOrdersLine.Quantity
            };
            return documentDTO;
        }
    }
    
}
