using Application.Abstractions;
using Application.Abstractions.Messaging;
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
    public sealed class GetPracticeToTakeQueryHandler : IQueryHandler<GetPracticeToTakeQuery, DoPracticeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPracticeToTakeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DoPracticeResponse> Handle(GetPracticeToTakeQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PracticeRepository.GetPracticeToTake(request.Id);
            return new DoPracticeResponse
            (
                Id: result.Id,
                Title: result.Title,
                Description: result.Description,
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
