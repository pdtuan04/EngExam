using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Exam;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Queries
{
    internal class GetExamCategoryPaginatedQueryHandler : IQueryHandler<GetExamCategoryPaginatedQuery, PaginationResponse<ExamCategoryResponse>>
    {
        private readonly IExamCategoryReadRepository _examCategoryReadRepository;
        public GetExamCategoryPaginatedQueryHandler(IExamCategoryReadRepository examCategoryReadRepository)
        {
            _examCategoryReadRepository = examCategoryReadRepository;
        }
        public async Task<PaginationResponse<ExamCategoryResponse>> Handle(GetExamCategoryPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await _examCategoryReadRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
