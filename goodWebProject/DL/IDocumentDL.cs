using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace DL
{
    public interface IDocumentDL
    {
        Task<Document> PostSaleOders(Document user);
        Task<Document> PostPurchasOders(Document user);
        Task<Document> UpdateDocumentSaleOders(Document d);
        Task<Document> UpdateDocumentPurchasOders(Document d);
    }
}
