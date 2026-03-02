using Application.Models.Exam;
using Application.Models.ExamCategory;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IExamService
    {
        Task<ExamDetailResponse> Create(CreateExamRequest request);
        Task<ExamDetailResponse> Update(UpdateExamRequest request);
        Task<bool> Delete(Guid id);
        Task<bool> SoftDelete(Guid id);
        Task<ExamResponse> GetById(Guid id);
        Task<TakeExamResponse> GetExamToTake(Guid id);
        Task<TakeExamResponse> GetRandomExamToTake();
        Task<ExamResultDetailResponse> SubmitExam(Guid userId, SubmitExamRequest request);
        Task<IEnumerable<ExamResponse>?> GetExamsByCategoryIdAsync(Guid id);
        Task<PaginationResponse<ExamResponse>> GetPaginated(PaginatedRequest request);
    }
}
