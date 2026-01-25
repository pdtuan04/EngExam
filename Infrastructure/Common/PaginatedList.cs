using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class PaginatedList<T> where T : class
    {
        public IEnumerable<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public PaginatedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public static async Task<PaginatedList<T>> QueryPageAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}
