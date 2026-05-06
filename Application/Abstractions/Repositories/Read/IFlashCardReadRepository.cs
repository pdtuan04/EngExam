using Application.Models.FlashCard;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IFlashCardReadRepository
    {
        public Task<IEnumerable<FlashCardResponse>> GetFlashCardsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        public Task<FlashCardDetailResponse> GetFlashCardDetailByIdAsync(Guid flashCardId, CancellationToken cancellationToken);
        Task UpsertAsync(FlashCardReadModel flashCard);
        Task DeleteAsync(Guid flashCardId, DateTime deletedAt);
    }
}
