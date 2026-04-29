using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.FlashCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Queries
{
    public sealed class GetFlashCardDetailByIdQueryHandler : IQueryHandler<GetFlashCardDetailByIdQuery, FlashCardDetailResponse>
    {
        private readonly IFlashCardReadRepository _flashCardReadRepository;
        public GetFlashCardDetailByIdQueryHandler(IFlashCardReadRepository flashCardReadRepository)
        {
            _flashCardReadRepository = flashCardReadRepository;
        }
        public async Task<FlashCardDetailResponse> Handle(GetFlashCardDetailByIdQuery request, CancellationToken cancellationToken)
        {
            return await _flashCardReadRepository.GetFlashCardDetailByIdAsync(request.FlashCardId, cancellationToken);
        }
    }
}
