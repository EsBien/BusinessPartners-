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
            Document documentToAdd = new Document ();
            if (document.documentType == document.saleTypeCode)
            {
     
                document.UserCode = getUserIdFromToken();
               documentToAdd = await _documentBL.PostSaleOders(document);
            }
         
            else if (document.documentType == document.purchasTypeCode) 
            {
                // var userId = HttpContext.Session.GetInt32("UserId");
                //document.UserCode = int.Parse(userId.ToString());
                document.UserCode = getUserIdFromToken();
                documentToAdd = await _documentBL.PostPurchasOders(document);
            }
            else { return BadRequest("not currect type was given!"); }
            if(document == null)
            {
                return BadRequest("cant add documet to db on or more fields are not valid");
            }
            return Ok(documentToAdd);
        }

        [HttpPut()]

        public async Task<ActionResult<Document>> Put([FromBody] Document document)
        {
            Document updateDocument = new Document ();
            document.ID = getUserIdFromToken();
            if (document.documentType == document.saleTypeCode) { 
                await _documentBL.UpdateDocumentSaleOders(document);
            }
            else if(document.documentType!= document.purchasTypeCode)
            {
                await _documentBL.UpdateDocumentPurchasOders(document);
            }
            //await _iuserBl.putUser(email, userToUpdate);
            if(updateDocument== null)
            {
                return BadRequest("error in updating document");
            }
            return Ok(updateDocument);
        }

    }
}
