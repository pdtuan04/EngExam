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
            var examResult = await _examResultReadRepository.GetDetailByIdAsync(request.Id);
            return new ExamResultDetailResponse(
                examResult.Id,
                examResult.CompleteAt, 
                examResult.Score, 
                UserAnswers: examResult.AnswerHistory
                                       .Select(a => new UserAnswerResponse(
                                           a.Question.Content,
                                           a.Question.QuestionTypes,
                                           a.UserAnswer,
                                           a.IsCorrect,
                                           a.Score,
                                           a.Question.Explanation ?? "",
                                           a.Question.Answers.Select(answer => new Option(
                                               answer.Content,
                                               answer.IsCorrect
                                           )).ToList())).ToList());
        }
    }
}
