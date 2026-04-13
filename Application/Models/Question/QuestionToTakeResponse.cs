using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public sealed record QuestionToTakeResponse(
        Guid Id,
        string Content,
        QuestionTypes QuestionTypes,
        IReadOnlyCollection<AnswerToTakeResponse> Answers)
    {
        public IReadOnlyCollection<AnswerToTakeResponse> Answers { get; init; } = Answers ?? [];
    }
}
