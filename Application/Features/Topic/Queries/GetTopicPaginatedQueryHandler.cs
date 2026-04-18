using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Pagination;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Queries
{
    public sealed class GetTopicPaginatedQueryHandler : IQueryHandler<GetTopicPaginatedQuery, PaginationResponse<TopicResponse>>
    {
        private readonly ITopicReadRepository _topicReadRepository;
        public GetTopicPaginatedQueryHandler(ITopicReadRepository topicReadRepository)
        {
            _topicReadRepository = topicReadRepository;
        }
        public async Task<PaginationResponse<TopicResponse>> Handle(GetTopicPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await _topicReadRepository.ToPagination<TopicResponse>(request.pageIndex, request.pageSize, cancellationToken: cancellationToken);
        }
    }
}
