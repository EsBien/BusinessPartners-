using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using DTO;
namespace DL
{
    public interface IDocumentDL
    {
        Task<Document> PostSaleOders(Document user);
        Task<Document> PostPurchasOders(Document user);
        Task<Document> UpdateDocumentSaleOders(Document d);
        Task<Document> UpdateDocumentPurchasOders(Document d);

        Task DeleteSaleOders(int id);
        Task DeletePurchasOders(int id);
        Task<DocumentDTO> GetDocumentSaleOder(int id);

        Task<DocumentDTO> GetDocumentPurchasOder(int id);
    }
}
