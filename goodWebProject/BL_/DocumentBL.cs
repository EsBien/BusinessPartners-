using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace BL_
{
    public class DocumentBL : IDocumentBL
    {
        IDocumentDL _documenta;
        public DocumentBL(IDocumentDL documenta)
        {
            _documenta = documenta;
        }

        public async Task<Document> PostPurchasOders(Document d)
        {
          
            return await _documenta.PostPurchasOders(d);
        }

        public async Task<Document> PostSaleOders(Document d)
        {
            return await _documenta.PostSaleOders(d);
        }

        public async Task<Document> UpdateDocumentPurchasOders(Document d)
        {
            throw new NotImplementedException();
        }

        public async Task<Document> UpdateDocumentSaleOders(Document d)
        {
            return await _documenta.UpdateDocumentSaleOders(d);
        }
    }
}
