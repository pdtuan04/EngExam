using Application.Abstractions;
using Application.Abstractions.Messaging;
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
        private readonly IUnitOfWork _unitOfWork;
        public GetExamResultPaginatedByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginationResponse<ExamResultResponse>> Handle(GetExamResultPaginatedByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ExamResultRepository.GetExamResultPaginatedByUserId(request.UserId, request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}
