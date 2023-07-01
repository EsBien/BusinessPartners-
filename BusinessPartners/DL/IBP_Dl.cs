using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace DL
{
    public interface IBP_Dl
    {
        Task<(IEnumerable<Bp> Records, int MaxPages)> ReadBP(string columnName = null, string filterValue = null, int page = 1, int pageSize = 10);

    }
}
