using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Behaviors;
using Application.Common.Exceptions;
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
    public sealed class GetExamByIdQueryHandler : IQueryHandler<GetExamByIdQuery, ExamDetailResponse>
    {
        private readonly IExamReadRepository _examReadRepository;
        public GetExamByIdQueryHandler(IExamReadRepository examReadRepository)
        {
            _examReadRepository = examReadRepository;
        }
        public async Task<ExamDetailResponse> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            return await _examReadRepository.GetExamDetail(request.Id) ?? throw new NotFoundException("Exam",request.Id);
        }
    }
}
