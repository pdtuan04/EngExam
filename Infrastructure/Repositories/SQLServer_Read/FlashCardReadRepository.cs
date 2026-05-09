using Application.Abstractions.Repositories.Read;
using Application.Models.FlashCard;
using Application.Models.Word;
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
    public sealed class FlashCardReadRepository : IFlashCardReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public FlashCardReadRepository(ApplicationDbReadContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid flashCardId, DateTime deletedAt)
        {
            var flashCard = await _dbContext.FlashCards
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(f => f.Id == flashCardId);
            if (flashCard != null)
            {
                if (flashCard.UpdatedAt >= deletedAt)
                {
                    return;
                }
                flashCard.IsDeleted = true;
                flashCard.UpdatedAt = deletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<FlashCardDetailResponse> GetFlashCardDetailByIdAsync(Guid flashCardId, CancellationToken cancellationToken)
        {
            var flashCard = await _dbContext.FlashCards
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == flashCardId, cancellationToken);
            var word = await _dbContext.Words
                .AsNoTracking()
                .Where(w => w.FlashCardId == flashCardId)
                .Select(w => new WordResponse(w.Id, w.Text, w.Meaning, w.CreatedAt,w.IsMemorized)).ToListAsync();

            return new FlashCardDetailResponse(flashCard.Id, flashCard.Title, flashCard.UserId,flashCard.Description, word);
        }

        public async Task<IEnumerable<FlashCardResponse>> GetFlashCardsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var flashCards = await _dbContext.FlashCards
                .Where(f => f.UserId == userId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<FlashCardResponse>>(flashCards);
        }

        public async Task UpsertAsync(FlashCardReadModel flashCard)
        {
            var existingFlashCard = await _dbContext.FlashCards
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(f => f.Id == flashCard.Id);
            if (existingFlashCard != null)
            {
                if(existingFlashCard.UpdatedAt >= flashCard.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(flashCard, existingFlashCard);
            }
            else
            {
                var newFlashCard = _mapper.Map<FlashCard>(flashCard);
                await _dbContext.FlashCards.AddAsync(newFlashCard);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
