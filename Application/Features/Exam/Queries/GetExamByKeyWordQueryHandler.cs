using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed class GetExamByKeyWordQueryHandler : IQueryHandler<GetExamByKeyWordQuery, IEnumerable<ExamSuggestResponse>>
    {
        private readonly IExamReadRepository _examReadRepository;
        public GetExamByKeyWordQueryHandler(IExamReadRepository examReadRepository)
        {
            _examReadRepository = examReadRepository;
        }
        public async Task<IEnumerable<ExamSuggestResponse>> Handle(GetExamByKeyWordQuery request, CancellationToken cancellationToken)
        {
            return await _examReadRepository.GetExamSuggestionsAsync(request.keyWord, cancellationToken);
        }
    }
}
