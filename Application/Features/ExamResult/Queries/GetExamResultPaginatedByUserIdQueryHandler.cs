using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed class GetExamResultPaginatedByUserIdQueryHandler : IQueryHandler<GetExamResultPaginatedByUserIdQuery, PaginationResponse<ExamResultResponse>>
    {
        private readonly IExamResultReadRepository _examResultReadRepository;
        public GetExamResultPaginatedByUserIdQueryHandler(IExamResultReadRepository examResultReadRepository)
        {
            _examResultReadRepository = examResultReadRepository;
        }
        public async Task<PaginationResponse<ExamResultResponse>> Handle(GetExamResultPaginatedByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _examResultReadRepository.GetExamResultPaginatedByUserId(request.UserId, request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}
