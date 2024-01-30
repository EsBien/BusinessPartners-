using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DTO;
using System.Threading;
using System.Reflection;

namespace DL
{
    public class BP_DL :IBP_Dl
    {
        BusinessPartnersContext  _context;

        public BP_DL(BusinessPartnersContext context) 
        {
            _context = context;
        }
   

        public async Task<Record> ReadBP(string columnName = null, string filterValue = null, int page = 1, int pageSize = 10)
        {
            Console.WriteLine("ReadBP begin on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);

            IQueryable<Bp> query = _context.Bps;
            Record record = new Record();

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(filterValue))
            {
                // Filter the records based on the specified column name and value
                query = query.Where(bp => EF.Property<string>(bp, columnName) == filterValue);
            }

            int totalRecords =await query.CountAsync();
            int maxPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Execute the query to retrieve the filtered and paginated records asynchronously
            record.records = query;
            record.maxPages = maxPages;
            record.totalPages = totalRecords;
            Console.WriteLine("ReadBP end on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);

            return record;
        }
    }
}
