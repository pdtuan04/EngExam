using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Queries
{
    public sealed class GetWordQueryHandler : IQueryHandler<GetWordQuery, WordResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetWordQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<WordResponse> Handle(GetWordQuery request, CancellationToken cancellationToken)
        {
            var word = await _unitOfWork.WordRepository.GetByTextAsync(request.Text);
            return new WordResponse(word.Id,word.Text,word.Meanings);
        }
    }
}
