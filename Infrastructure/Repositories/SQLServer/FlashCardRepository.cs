using Application.Abstractions.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class FlashCardRepository : GenericRepository<Domain.Entity.FlashCard, FlashCard>, IFlashCardRepository
    {
        public FlashCardRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}
