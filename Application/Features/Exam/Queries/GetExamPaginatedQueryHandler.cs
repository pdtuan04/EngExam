using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Exam;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed class GetExamPaginatedQueryHandler : IQueryHandler<GetExamPaginatedQuery, PaginationResponse<ExamResponse>>
    {
        private readonly IExamReadRepository _examReadRepository;
        public GetExamPaginatedQueryHandler(IExamReadRepository examReadRepository)
        {
            _examReadRepository = examReadRepository;
        }
        public async Task<PaginationResponse<ExamResponse>> Handle(GetExamPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await _examReadRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
