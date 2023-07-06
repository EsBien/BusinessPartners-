using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Record
    {
        public int paginated { get; set; }
        public int maxPages { get; set; }

        public int totalPages { get; set; }
        public IEnumerable<Object> records { get; set; }
    }
}
