using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Pagination
{
    public class PaginationResponse<T> 
    {
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; } 
        public int PageSize { get; init; } 
        public int TotalCount { get; init; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public List<T>? Items { get; init; }
        public PaginationResponse() { }
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
