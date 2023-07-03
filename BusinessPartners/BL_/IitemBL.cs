using DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_
{
    public interface IitemBL
    {
        Task<Record> ReadItems(string columnName = null, string filterValue = null, int page = 1, int pageSize = 10);
    }
}
