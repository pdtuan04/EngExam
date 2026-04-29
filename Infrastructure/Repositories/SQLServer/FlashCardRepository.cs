using Application.Abstractions.Repositories;
using Application.Common.Exceptions;
using Application.Models.FlashCard;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Domain.Entity.FlashCard> GetFlashCardDetailAsync(Guid id)
        {
            var flashCard = await _dbContext.FlashCards.Include(x => x.Words).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Domain.Entity.FlashCard>(flashCard);
        }
    }
}
