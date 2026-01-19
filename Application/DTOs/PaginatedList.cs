using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaginatedList<T> where T : class
    {
        public IEnumerable<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasNextPage => PageNumber * PageSize < TotalCount;
        public bool HasPreviousPage => PageNumber > 1;
        public PaginatedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public static async Task<PaginatedList<T>> CreatePageAsync(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            return new PaginatedList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}
