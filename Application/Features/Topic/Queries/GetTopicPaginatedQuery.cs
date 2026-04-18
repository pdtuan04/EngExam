using Application.Abstractions.Messaging;
using Application.Models.Pagination;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Queries
{
    public sealed record GetTopicPaginatedQuery(int pageIndex, int pageSize) : IQuery<PaginationResponse<TopicResponse>>;
}
