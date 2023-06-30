using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Entities;
using BL_;
using DL;
using System.Linq;

namespace MyFirstWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentController : ControllerBase
    {

        IDocumentBL _documentBL;
        public DocumentController(IDocumentBL documentBL)
        {
            _documentBL = documentBL;
        }
        private int getUserIdFromToken()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            // Retrieve the claims from the token
            var claims = HttpContext.User.Claims.ToList();

            string userId = HttpContext.User.FindFirst(c => c.Type.EndsWith("nameidentifier"))?.Value;
            return int.Parse(userId);
        }
        [HttpPost]

        public async Task<ActionResult<Document>> Post([FromBody] Document document)
        {
            Document documentToAdd = new Document();
            document.UserCode = getUserIdFromToken();
            if (document.documentType == DocumentType.SaleOrders)
            {
                documentToAdd = await _documentBL.PostSaleOders(document);
            }

            else if (document.documentType == DocumentType.PurchaseOrders)
            {
                // var userId = HttpContext.Session.GetInt32("UserId");
                //document.UserCode = int.Parse(userId.ToString());
                document.UserCode = getUserIdFromToken();
                documentToAdd = await _documentBL.PostPurchasOders(document);
            }
            else { return BadRequest("not currect type was given!"); }
            if (document == null)
            {
                return BadRequest("cant add documet to db on or more fields are not valid");
            }
            return Ok(documentToAdd);
        }

        [HttpPut()]

        public async Task<ActionResult<Document>> Put([FromBody] Document document)
        {
            Document updateDocument = new Document();
            document.UserCode = getUserIdFromToken();
            if (document.documentType == DocumentType.SaleOrders)
            {
                await _documentBL.UpdateDocumentSaleOders(document);
            }
            else if (document.documentType != DocumentType.PurchaseOrders)
            {
                await _documentBL.UpdateDocumentPurchasOders(document);
            }
            if (updateDocument == null)
            {
                return BadRequest("error in updating document");
            }
            return Ok(updateDocument);
        }

        [HttpDelete("{id}/{type}")]
        public async Task<ActionResult> Delete(int id, string type)
        {
            
            if (type == DocumentType.SaleOrders)
            {
                await _documentBL.DeleteSaleOders(id);
            }
            else if(type==DocumentType.PurchaseOrders)
            {
                await _documentBL.DeletePurchasOders(id);
            }
            else
            {
                return BadRequest("no such type in DB");
            }
            return Ok();
        }


        [HttpGet("{id}/{type}")]
        [AllowAnonymous]
        public async Task<ActionResult<DocumentDTO>> Get(int id,string type)
        {
            DocumentDTO newDocument=null;
            if (type == DocumentType.SaleOrders)
            {
                newDocument =await _documentBL.GetDocumentSaleOder(id);
                newDocument.documentType= type;
            }
            else if (type == DocumentType.PurchaseOrders)
            {
                newDocument =await _documentBL.GetDocumentPurchasOder(id);
                newDocument.documentType = type;
            }
            else
            {
                return BadRequest("no such type in DB");
            }
            if (newDocument == null)
                BadRequest("no document in db");
            
            return Ok(newDocument);
        }

    }
}
