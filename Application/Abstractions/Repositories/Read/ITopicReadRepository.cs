using Application.Models.Pagination;
using Application.Models.Topic;

namespace Application.Abstractions.Repositories.Read
{
    public interface ITopicReadRepository
    {
        Task<IEnumerable<TopicResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task UpsertAsync(TopicReadModel topic);
        Task DeleteAsync(Guid topicId, DateTime actionAt);
        Task<PaginationResponse<TopicResponse>> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
