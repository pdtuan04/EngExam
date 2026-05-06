using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.ExamResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed class GetExamResultDetailsQueryHandler : IQueryHandler<GetExamResultDetailsQuery, ExamResultDetailResponse>
    {
        private readonly IExamResultReadRepository _examResultReadRepository;
        public GetExamResultDetailsQueryHandler(IExamResultReadRepository examResultReadRepository)
        {
            _examResultReadRepository = examResultReadRepository;
        }
        public async Task<ExamResultDetailResponse> Handle(GetExamResultDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _examResultReadRepository.GetDetailByIdAsync(request.Id);
        }
    }
}
