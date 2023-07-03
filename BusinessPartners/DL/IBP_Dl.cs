using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DTO;
namespace DL
{
    public interface IBP_Dl
    {
        Task<Record> ReadBP(string columnName = null, string filterValue = null, int page = 1, int pageSize = 10);

    }
}
