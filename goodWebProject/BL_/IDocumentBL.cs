using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace BL_
{
    public interface IDocumentBL
    {
        Task<Document> PostSaleOders(Document user);
        Task<Document> PostPurchasOders(Document user);

        Task<Document> UpdateDocumentSaleOders(Document d);
        Task<Document> UpdateDocumentPurchasOders(Document d);
    }
}
