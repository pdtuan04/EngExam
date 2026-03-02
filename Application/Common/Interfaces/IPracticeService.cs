using Application.Models.Pagination;
using Application.Models.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPracticeService
    {
        Task<DoPracticeResponse> GetPracticeToTake(Guid id);
        Task<PaginationResponse<PracticeResponse>> GetPaginated(int pageIndex, int pageSize);
    }
}
