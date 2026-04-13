using Application.Models.Answer;
using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public record QuestionDetailResponse(
        Guid Id,
        DateTime CreateAt,
        string Content,
        QuestionTypes QuestionTypes,
        double Score,
        Guid TopicId,
        string? Explanation = null,
        string? ImageUrl = null,
        IReadOnlyCollection<AnswerDetailsResponse> Answers = null!)
    {
        public IReadOnlyCollection<AnswerDetailsResponse> Answers { get; init; } = Answers ?? [];
    }
}
