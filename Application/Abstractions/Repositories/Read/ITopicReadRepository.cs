using Application.Models.Topic;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface ITopicReadRepository : IGenericReadRepository<Topic>
    {
        Task<IEnumerable<TopicResponse>> GetAllAsync(CancellationToken cancellationToken);
    }
}
