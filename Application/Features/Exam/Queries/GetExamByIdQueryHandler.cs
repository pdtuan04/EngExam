using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Behaviors;
using Application.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed class GetExamByIdQueryHandler : IQueryHandler<GetExamByIdQuery, ExamResponse>
    {
        private readonly IExamReadRepository _examReadRepository;
        public GetExamByIdQueryHandler(IExamReadRepository examReadRepository)
        {
            _examReadRepository = examReadRepository;
        }
        public async Task<ExamResponse> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _examReadRepository.GetByIdAsync(request.Id);
            return new ExamResponse(result.Id, result.Title, result.Description, result.DurationInMinutes, result.ExamCategoryId, result.CreatedAt);
        }
    }
}
