
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
using DTO;
namespace MyFirstWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BPController : Controller
    {
        IBP_BL _bp;
        public BPController(IBP_BL bp)
        {
            _bp = bp;
        }
        [HttpGet]

        public async Task<ActionResult<Record>> ReadBP([FromQuery] string columnName = null, [FromQuery] string filterValue = null)
        {
 
            Record record = null;
            record = await _bp.ReadBP(columnName, filterValue);
            if (record == null)
            {
                return NotFound("no record found");
            }
            return Ok(record);

        }

    }

}
