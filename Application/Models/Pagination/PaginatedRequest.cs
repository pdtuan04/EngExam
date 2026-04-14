using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Pagination
{
    public sealed record PaginatedRequest(
    int PageIndex = 1,
    int PageSize = 10);
}
