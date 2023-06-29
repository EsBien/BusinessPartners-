﻿using Entities;
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
        BusinessPartnersContext _contextl;
        public DocumentDL(BusinessPartnersContext businessPartnersContext) { 
                _contextl = businessPartnersContext;
        }

        private bool checkDocument(Document d , string bpTpe)
        {
            Bp getBP = _contextl.Bps.FindAsync(d.BPCode).Result;
            Item item = _contextl.Items.FindAsync(d.IteamCode).Result;
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
            await _contextl.PurchaseOrders.AddAsync(purchaseOrder);
            await _contextl.SaveChangesAsync();
            var purchaseOrderId = _contextl.PurchaseOrders.FirstOrDefaultAsync(p => p.Bpcode== purchaseOrder.Bpcode &&
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
            await _contextl.PurchaseOrdersLines.AddAsync(purchaseOrdersLine);
            await _contextl.SaveChangesAsync();
            d.setCreateDate(purchaseOrder.CreateDate);
            d.setCreateDate(purchaseOrder.CreateDate);
            d.LastUpdateDate = null;
            d.LastUpdateDateBy = null;
            return d;
        }

        public async Task<Document> PostSaleOders(Document d)
        {
            if (checkDocument(d,"V"))
            {
                return null;
            }

            ////var userId = HttpContext.Session.GetInt32("UserId");
            SaleOrder saleOrder = new SaleOrder
            {
                Bpcode = d.BPCode,
                CreateDate = DateTime.Now,
                LastUpdateDate = null,
                CreatedBy = d.UserCode,
                LastUpdatedBy = null
            };
            await _contextl.SaleOrders.AddAsync(saleOrder);
            await _contextl.SaveChangesAsync();
           
            var saleOrderId = _contextl.SaleOrders.FirstOrDefaultAsync(p => p.Bpcode == saleOrder.Bpcode &&
            p.CreateDate == saleOrder.CreateDate && p.CreatedBy == saleOrder.CreatedBy).Result.Id;
            SaleOrdersLine SaleOrdersLine = new SaleOrdersLine
            {
                DocId = saleOrderId,
                Quantity = d.Quantity,
                CreateDate = DateTime.Now,
                LastUpdateDate = null,
                CreatedBy = d.UserCode,
                LastUpdatedBy = null
            };
            await _contextl.SaleOrdersLines.AddAsync(SaleOrdersLine);
            await _contextl.SaveChangesAsync();
            d.setCreateDate(saleOrder.CreateDate);
            d.setCreateDateBy(saleOrder.CreateDate);
            d.LastUpdateDate = null;
            d.LastUpdateDateBy = null;
            return d;
        }



    }
}
