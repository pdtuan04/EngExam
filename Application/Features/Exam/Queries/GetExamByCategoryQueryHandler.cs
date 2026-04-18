using Application.Abstractions;
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
    public sealed class GetExamByCategoryQueryHandler: IQueryHandler<GetExamByCategoryQuery, IEnumerable<ExamResponse>>
    {
        private readonly IExamReadRepository _examReadRepository;
        public GetExamByCategoryQueryHandler(IExamReadRepository examReadRepository)
        {
            _examReadRepository = examReadRepository;
        }
        public async Task<IEnumerable<ExamResponse>> Handle(GetExamByCategoryQuery request, CancellationToken cancellationToken)
        {
            var exams = await _examReadRepository.GetExamsByCategoryIdAsync(request.CategoryId);
            return exams.Select(e => new ExamResponse
            (
                Id: e.Id,
                Title: e.Title,
                Description: e.Description,
                DurationInMinutes: e.DurationInMinutes,
                ExamCategoryId: e.ExamCategoryId,
                CreatedAt: e.CreatedAt
            ));
        }
    }
}
