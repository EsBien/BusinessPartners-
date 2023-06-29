using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BL_;
namespace MyFirstWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        IitemBL _iitemBL;
        public ItemController(IitemBL iitemBL) {
            _iitemBL = iitemBL;
        }
        [HttpGet]

        public async Task<ActionResult<(IEnumerable<Bp> Records, int MaxPages)>> ReadItems([FromQuery] string columnName = null, [FromQuery] string filterValue = null)
        {
            IEnumerable<Bp> records;
            int rows = 0;
            (records, rows) = await _iitemBL.ReadItems(columnName, filterValue);
            if(records == null)
            {
                return NotFound("no record found");
            }
            return Ok(records);

        }
       
    }
    
}
