using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.FlashCard;

namespace Application.Features.FlashCard.Queries
{
    public sealed class GetFlashCardByUserIdQueryHandler : IQueryHandler<GetFlashCardByUserIdQuery, IEnumerable<FlashCardResponse>>
    {
        private readonly IFlashCardReadRepository _flashCardReadRepository;
        public GetFlashCardByUserIdQueryHandler(IFlashCardReadRepository flashCardReadRepository)
        {
            _flashCardReadRepository = flashCardReadRepository;
        }
        public async Task<IEnumerable<FlashCardResponse>> Handle(GetFlashCardByUserIdQuery request, CancellationToken cancellationToken)
        {
            var flashCards = await _flashCardReadRepository.GetFlashCardsByUserIdAsync(request.UserId, cancellationToken);
            return flashCards.Select(f => new FlashCardResponse(f.Id, f.Title, f.Description,f.UserId));
        }
    }
}
