using Application.Abstractions.Repositories.Read;
using Application.Models.FlashCard;
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

        public async Task<FlashCardDetailResponse> GetFlashCardDetailByIdAsync(Guid flashCardId, CancellationToken cancellationToken)
        {
            var flashCard = 
                await _dbContext.FlashCards
                .Include(f => f.Words)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == flashCardId, cancellationToken);
            return _mapper.Map<FlashCardDetailResponse>(flashCard);
        }

        public async Task<IEnumerable<FlashCardResponse>> GetFlashCardsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var flashCards = await _dbContext.FlashCards
                .Where(f => f.UserId == userId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<FlashCardResponse>>(flashCards);
        }
    }
}
