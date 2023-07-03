using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using DTO;

namespace BL_ { 

    public class ItemBL : IitemBL
    {
            IitemDL _IitemDL;
        public ItemBL(IitemDL IitemDL) {
            _IitemDL = IitemDL;
        }

        public async Task<Record> ReadItems(string columnName = null, string filterValue = null, int page = 1, int pageSize = 10)
        {
        

            return await _IitemDL.ReadItems(columnName, filterValue, page, pageSize);

        }
       

     
    }
}
