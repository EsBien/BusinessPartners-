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
    [Authorize]
    public class ItemController : ControllerBase
    {
        IitemBL _iitemBL;
        public ItemController(IitemBL iitemBL) {
            _iitemBL = iitemBL;
        }

        [HttpGet]

        public async Task<ActionResult<Record>> ReadItems([FromQuery] string columnName = null, [FromQuery] string filterValue = null)
        {
           
            Record record = null;
            record = await _iitemBL.ReadItems(columnName, filterValue);
          
            if (record == null)
            {
                return NotFound("no record found");
            }
            return Ok(record);

        }
       
    }
    
}
