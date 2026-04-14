using Application.Abstractions;
using Application.Abstractions.Messaging;
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
        private readonly IUnitOfWork _unitOfWork;
        public GetExamToTakeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TakeExamResponse> Handle(GetExamToTakeQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExamRepository.GetExamToTake(request.Id);
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
