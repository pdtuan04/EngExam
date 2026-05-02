using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Queries
{
    public sealed class GetAllTopicQueryHandler : IQueryHandler<GetAllTopicQuery, IEnumerable<TopicResponse>>
    {
        private readonly ITopicReadRepository _topicReadRepository;
        public GetAllTopicQueryHandler(ITopicReadRepository topicReadRepository)
        {
            _topicReadRepository = topicReadRepository;
        }
        public async Task<IEnumerable<TopicResponse>> Handle(GetAllTopicQuery request, CancellationToken cancellationToken)
        {
            return await _topicReadRepository.GetAllAsync(cancellationToken);
        }
    }
}
