using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed class GetExamToTakeQueryHandler : IQueryHandler<GetExamToTakeQuery, TakeExamResponse>
    {
        private readonly IExamReadRepository _examReadRepository;
        public GetExamToTakeQueryHandler(IExamReadRepository examReadRepository)
        {
            _examReadRepository = examReadRepository;
        }
        public async Task<TakeExamResponse> Handle(GetExamToTakeQuery request, CancellationToken cancellationToken)
        {
            return await _examReadRepository.GetExamToTake(request.Id);
        }
    }
}
