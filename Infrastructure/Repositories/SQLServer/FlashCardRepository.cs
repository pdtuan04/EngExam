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
    public class FlashCardRepository : GenericRepository<Domain.Entity.FlashCard, FlashCard, Guid>, IFlashCardRepository
    {
        public FlashCardRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Domain.Entity.FlashCard> GetFlashCardDetailAsync(Guid id)
        {
            var flashCard = await _dbContext.FlashCards
                                            .Include(x => x.Words)
                                            .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Domain.Entity.FlashCard>(flashCard);
        }
        public override async Task<Domain.Entity.FlashCard> Update(Domain.Entity.FlashCard entity)
        {
            var flashCard = await _dbContext.FlashCards.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (flashCard == null)
            {
                throw new NotFoundException("FlashCard", entity.Id);
            }
            flashCard.Title = entity.Title;
            flashCard.Description = entity.Description;
            return _mapper.Map<Domain.Entity.FlashCard>(flashCard);
        }
    }
}
