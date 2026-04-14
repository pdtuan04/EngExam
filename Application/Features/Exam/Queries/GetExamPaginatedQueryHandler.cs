using Application.Abstractions;
using Application.Abstractions.Messaging;
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
        private readonly IUnitOfWork _unitOfWork;
        public GetExamPaginatedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginationResponse<ExamResponse>> Handle(GetExamPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ExamRepository.ToPagination<ExamResponse>(request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        }
    }
}
