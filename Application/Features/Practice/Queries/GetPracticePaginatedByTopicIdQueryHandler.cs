using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Models.Pagination;
using Application.Models.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Queries
{
    public sealed class GetPracticePaginatedByTopicIdQueryHandler : IQueryHandler<GetPracticePaginatedByTopicIdQuery, PaginationResponse<PracticeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPracticePaginatedByTopicIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginationResponse<PracticeResponse>> Handle(GetPracticePaginatedByTopicIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PracticeRepository.GetPracticePaginatedByTopicIdAsync(request.TopicId, request.pageIndex, request.pageSize, cancellationToken);
        }
    }
}
