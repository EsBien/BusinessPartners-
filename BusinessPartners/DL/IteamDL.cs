using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DTO;
using Microsoft.Identity.Client;

namespace DL
{
    public class IteamDL :IitemDL
    {
        BusinessPartnersContext _context;

        public IteamDL(BusinessPartnersContext context)
        {
           _context = context;
        }

        public async Task<Record> ReadItems(string columnName = null, string filterValue = null, int page = 1,int pageSize=10)
        {
            Record record = new Record();
            IQueryable<Item> query = _context.Items;

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(filterValue))
            {
                // Filter the records based on the specified column name and value
                query = query.Where(Item => EF.Property<string>(Item, columnName) == filterValue);
            }

            int totalRecords = await query.CountAsync();
            int maxPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Execute the query to retrieve the filtered and paginated records asynchronously
            List<Item> records = await query.ToListAsync();
            record.records = query;
            record.maxPages=maxPages;
            record.totalPages = totalRecords;

            return record;
        }


    }
        
    
    
}
