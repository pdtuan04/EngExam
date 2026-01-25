using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaginatedDTO
    {
        public string? search { get; set; }
        public SortDirection sortDir { get; set; } = SortDirection.None;
        public string? sortBy { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
    public enum SortDirection
    {
        None,
        Ascending,
        Descending
    }
}
