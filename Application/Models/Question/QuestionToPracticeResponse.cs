using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public sealed record QuestionToPracticeResponse(
    Guid Id,
    string Content,
    QuestionTypes QuestionTypes,
    string? Explanation = null,
    string? ImageUrl = null,
    IReadOnlyCollection<AnswerToPracticeResponse> Answers = null!)
    {
        public IReadOnlyCollection<AnswerToPracticeResponse> Answers { get; init; } = Answers ?? [];
    }
}
