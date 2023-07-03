using DL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BL_
{
    public class BP_BL : IBP_BL
    {
        IBP_Dl _bp;
        public BP_BL(IBP_Dl bP_BL) {
            _bp = bP_BL;
        }
        public async Task<Record> ReadBP(string columnName = null, string filterValue = null, int page = 1, int pageSize = 10)
        {
            return await _bp.ReadBP(columnName, filterValue, page, pageSize);
        }
    }
}
