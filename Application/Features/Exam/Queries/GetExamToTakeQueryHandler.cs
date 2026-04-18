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
            var result = await _examReadRepository.GetExamToTake(request.Id);
            return new TakeExamResponse
            (
                Id: result.Id,
                Title: result.Title,
                Description: result.Description,
                DurationInMinutes: result.DurationInMinutes,
                Questions: result.ExamDetail.Select(q => new QuestionToTakeResponse
                (
                    Id: q.QuestionId,
                    Content: q.Question.Content,
                    QuestionTypes: q.Question.QuestionTypes,
                    Answers: q.Question.Answers.Select(o => new AnswerToTakeResponse
                    (
                        Id: o.Id,
                        Content: o.Content
                    )).ToList()
                )).ToList()
            );
        }
    }
}
