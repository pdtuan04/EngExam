using Application.Models.FlashCard;
using Domain.Entity;

namespace Application.Abstractions.Repositories
{
    public interface IFlashCardRepository : IGenericRepository<FlashCard>
    {
        Task<FlashCard> GetFlashCardDetailAsync(Guid id);
    }
}
