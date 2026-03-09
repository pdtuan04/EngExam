using Application.Models.Answer;
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
        Task<ExamCategoryResponse> GetById(Guid id);
        Task<ExamCategoryResponse> CreateExamCategogy(CreateExamCategoryRequest request);
        Task<ExamCategoryResponse> UpdateExamCategory(UpdateExamCategoryRequest request);
        Task DeleteExamCategogy(Guid id);
    }
}
