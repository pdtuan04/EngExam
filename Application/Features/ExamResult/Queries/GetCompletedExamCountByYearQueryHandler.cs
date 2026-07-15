using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed class GetCompletedExamCountByYearQueryHandler : IQueryHandler<GetCompletedExamCountByYearQuery, int>
    {
        private readonly IExamResultReadRepository _examResultReadRepository;
        public GetCompletedExamCountByYearQueryHandler(IExamResultReadRepository examResultReadRepository)
        {
            _examResultReadRepository = examResultReadRepository;
        }
        public async Task<int> Handle(GetCompletedExamCountByYearQuery request, CancellationToken cancellationToken)
        {
            return await _examResultReadRepository.GetCompletedExamCountByYearAsync(request.Year, cancellationToken);
        }
    }
}
