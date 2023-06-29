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

        [HttpPost]

        public async Task<ActionResult<Document>> Post([FromBody] Document document)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Retrieve the claims from the token
            var claims = HttpContext.User.Claims.ToList();
            string userId1 = claims.FirstOrDefault(c => c.Type == "nameidentifier")?.Value;
            string userId2 = claims.FirstOrDefault(c => c.Type == "name")?.Value;
            //string userId = HttpContext.User.FindFirst(userIdClaimType)?.Value; claims[0].Type.EndsWith("nameidentifier") nameidentifier
            Document documentToAdd = new Document ();
            if (document.documentType == document.saleType)
            {
     
                document.UserCode = int.Parse(userId1);
               documentToAdd = await _documentBL.PostSaleOders(document);
            }
         
            else if (document.documentType == document.purchasType)
            {
                //var userId = HttpContext.Session.GetInt32("UserId");
                //document.UserCode = int.Parse(userId.ToString());
                document.UserCode = int.Parse(userId1);
                documentToAdd = await _documentBL.PostPurchasOders(document);
            }
            else { return BadRequest("not currect type was given!"); }
       
            return Ok(documentToAdd);
        }

    }
}
