using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Pagination
{
    public class PaginationResponse<T> 
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; } 
        public int PageSize { get; private set; } 
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public List<T>? Items { get; private set; } 
        public PaginationResponse(List<T>? items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
