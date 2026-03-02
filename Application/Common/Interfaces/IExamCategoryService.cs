using Application.Models.ExamCategory;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IExamCategoryService
    {
        Task<ICollection<ExamCategoryResponse>> GetAll();
        Task<PaginationResponse<ExamCategoryResponse>> GetPaginated(PaginatedRequest request);
    }
}
