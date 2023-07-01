﻿using Microsoft.AspNetCore.Http;
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
    public class Document
    {
      
        public int ID { get; set; }
        public string documentType { get; set; }
        public string BPCode { get; set; }
        public string IteamCode { get; set; }
        public int UserCode { get; set; }
        public string Comment { get; set; }
        public DateTime? createDate;

        private bool isCreateDateModified = false;

        private DateTime? CreateDate { get; set; }
     
        public void setCreateDate(DateTime ? d)
        {
            if (!isCreateDateModified)
            {
                createDate = DateTime.Now;
                isCreateDateModified = true;
            }
            
        }
        public DateTime? getCreateDate()
        {
            return CreateDate;

        }

        public DateTime? LastUpdateDate { get; set; }

        private bool isCreateDateByModified = false;
        private DateTime? CreateDateBy { get; set; }

        public void setCreateDateBy(DateTime? d)
        {
            if (!isCreateDateByModified)
            {
                CreateDateBy = DateTime.Now;
                isCreateDateByModified = true;
            }

        }
        public DateTime? getCreateDateBy()
        {
            return CreateDateBy;

        }

        public DateTime? LastUpdateDateBy { get; set; }

        public decimal Quantity { get; set; }
    }
}