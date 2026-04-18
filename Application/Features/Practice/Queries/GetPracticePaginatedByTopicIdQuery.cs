using Application.Abstractions.Messaging;
using Application.Models.Pagination;
using Application.Models.Practice;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Queries
{
    public sealed record GetPracticePaginatedByTopicIdQuery(int pageIndex, int pageSize, Guid TopicId) : IQuery<PaginationResponse<PracticeResponse>>;
}
