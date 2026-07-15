using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed class GetCompletedExamCountByMonthQueryHandler : IQueryHandler<GetCompletedExamCountByMonthQuery, int>
    {
        private readonly IExamResultReadRepository _examResultReadRepository;
        public GetCompletedExamCountByMonthQueryHandler(IExamResultReadRepository examResultReadRepository)
        {
            _examResultReadRepository = examResultReadRepository;
        }
        public async Task<int> Handle(GetCompletedExamCountByMonthQuery request, CancellationToken cancellationToken)
        {
            return await _examResultReadRepository.GetCompletedExamCountByMonthAsync(request.Year, request.Month, cancellationToken);
        }
    }
}
