using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public sealed class TopicReadRepository : GenericReadRepository<Domain.Entity.Topic, Topic>, ITopicReadRepository
    {
        public TopicReadRepository(ApplicationDbReadContext context, IMapper mapper) : base(context, mapper) 
        {
        }
    }
}
