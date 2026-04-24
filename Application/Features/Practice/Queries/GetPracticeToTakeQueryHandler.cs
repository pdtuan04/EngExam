using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Answer;
using Application.Models.Practice;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Queries
{
    public sealed class GetPracticeToTakeQueryHandler : IQueryHandler<GetPracticeToTakeQuery, PracticeDetailResponse>
    {
        private readonly IPracticeReadRepository _practiceReadRepository;
        public GetPracticeToTakeQueryHandler(IPracticeReadRepository practiceReadRepository)
        {
            _practiceReadRepository = practiceReadRepository;
        }
        public async Task<PracticeDetailResponse> Handle(GetPracticeToTakeQuery request, CancellationToken cancellationToken)
        {
            var result = await _practiceReadRepository.GetPracticeToTake(request.Id) ?? throw new NullReferenceException();
            return new PracticeDetailResponse
            (
                Id: result.Id,
                Title: result.Title,
                Description: result.Description,
                TopicId: result.TopicId,
                CreatedAt: result.CreatedAt,
                Questions: result.PracticeDetails.Select(x => new QuestionToPracticeResponse
                (
                    Id: x.QuestionId,
                    Content: x.Question.Content,
                    Explanation: x.Question.Explanation,
                    ImageUrl: x.Question.ImageUrl,
                    QuestionTypes: x.Question.QuestionTypes,
                    Answers: x.Question.Answers.Select(a => new AnswerToPracticeResponse
                    (
                        Id: a.Id,
                        Content: a.Content,
                        IsCorrect: a.IsCorrect
                    )).ToList()
                )).ToList()
            );
        }
    }
}
