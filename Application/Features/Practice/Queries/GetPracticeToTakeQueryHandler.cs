using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Common.Exceptions;
using Application.Models.Answer;
using Application.Models.Practice;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Queries
{
    public sealed class GetPracticeToTakeQueryHandler : IQueryHandler<GetPracticeToTakeQuery, PracticeDetailResponse>
    {
        private readonly IPracticeReadRepository _practiceReadRepository;
        public GetPracticeToTakeQueryHandler(IPracticeReadRepository practiceReadRepository)
        {
            _practiceReadRepository = practiceReadRepository;
        }
        public async Task<PracticeDetailResponse> Handle(GetPracticeToTakeQuery request, CancellationToken cancellationToken)
        {
            return await _practiceReadRepository.GetPracticeToTake(request.Id) ?? throw new NotFoundException("Practice",request.Id);
        }
    }
}
