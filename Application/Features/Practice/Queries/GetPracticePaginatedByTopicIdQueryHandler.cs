using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
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
        private readonly IPracticeReadRepository _practiceReadRepository;
        public GetPracticePaginatedByTopicIdQueryHandler(IPracticeReadRepository practiceReadRepository)
        {
            _practiceReadRepository = practiceReadRepository;
        }
        public async Task<PaginationResponse<PracticeResponse>> Handle(GetPracticePaginatedByTopicIdQuery request, CancellationToken cancellationToken)
        {
            return await _practiceReadRepository.GetPracticePaginatedByTopicIdAsync(request.TopicId, request.pageIndex, request.pageSize, cancellationToken);
        }
    }
}
