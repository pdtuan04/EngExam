using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.Pagination;
using Application.Models.Practice;
using Application.Models.Topic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public sealed class TopicReadRepository : ITopicReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public TopicReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid topicId, DateTime actionAt)
        {
            var topic = await _dbContext.Topics
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(t => t.Id == topicId);
            if (topic != null)
            {
                if (topic.UpdatedAt >= actionAt)
                {
                    return;
                }
                topic.IsDeleted = true;
                topic.UpdatedAt = actionAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TopicResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var topics = await _dbContext.Topics.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TopicResponse>>(topics);
        }

        public async Task<PaginationResponse<TopicResponse>> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Topics.AsNoTracking();
            var projectedQuery = query.ProjectTo<TopicResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<TopicResponse>.ToPagedList(projectedQuery, pageIndex, pageSize);
            return new PaginationResponse<TopicResponse>(queryExecute.Items, queryExecute.TotalCount, pageIndex, pageSize);
        }

        public async Task UpsertAsync(TopicReadModel topic)
        {
            var existingTopic = await _dbContext.Topics.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == topic.Id);
            if (existingTopic != null)
            {
                if(existingTopic.UpdatedAt >= topic.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(topic, existingTopic);
            }
            else
            {
                var newTopic = _mapper.Map<Topic>(topic);
                newTopic.IsDeleted = false;
                _dbContext.Topics.Add(newTopic);
            }
            await _dbContext.SaveChangesAsync();
        }

    }
}
