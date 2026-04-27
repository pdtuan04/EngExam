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
    public sealed class WordReadRepository : GenericReadRepository<Domain.Entity.Word, Word>, IWordReadRepository
    {
        public WordReadRepository(ApplicationDbReadContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
