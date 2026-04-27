using Application.Abstractions.Repositories.Read;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public sealed class FlashCardReadRepository : GenericReadRepository<Domain.Entity.FlashCard, FlashCard>, IFlashCardReadRepository
    {
        public FlashCardReadRepository(ApplicationDbReadContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Domain.Entity.FlashCard> GetFlashCardDetailByIdAsync(Guid flashCardId)
        {
            var flashCard = 
                await _dbContext.FlashCards
                .Include(f=>f.Words)
                .FirstOrDefaultAsync(f => f.Id == flashCardId);
            return _mapper.Map<Domain.Entity.FlashCard>(flashCard);
        }

        public async Task<IEnumerable<Domain.Entity.FlashCard>> GetFlashCardsByUserIdAsync(Guid userId)
        {
            var flashCards = await _dbContext.FlashCards
                .Where(f => f.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.FlashCard>>(flashCards);
        }
    }
}
