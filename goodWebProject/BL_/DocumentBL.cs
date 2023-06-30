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
        IDocumentDL _document;
        public DocumentBL(IDocumentDL documenta)
        {
            _document = documenta;
        }

        public async Task DeleteSaleOders(int id)
        {
            await _document.DeleteSaleOders(id);
        }

        public async Task<Document> PostPurchasOders(Document d)
        {
          
            return await _document.PostPurchasOders(d);
        }

        public async Task<Document> PostSaleOders(Document d)
        {
            return await _document.PostSaleOders(d);
        }

        public async Task<Document> UpdateDocumentPurchasOders(Document d)
        {
            return await _document.UpdateDocumentPurchasOders(d);
        }

        public async Task<Document> UpdateDocumentSaleOders(Document d)
        {
            return await _document.UpdateDocumentSaleOders(d);
        }
    }
}
