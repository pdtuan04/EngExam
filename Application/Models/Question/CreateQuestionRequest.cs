
using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public record CreateQuestionRequest(
        string Content,
        double Score,
        QuestionTypes QuestionTypes,
        Guid TopicId,
        string? Explanation = null,
        string? AudioUrl = null,
        string? ImageUrl = null,
        IReadOnlyCollection<CreateAnswerRequest> Answers = null!)
    {
        public IReadOnlyCollection<CreateAnswerRequest> Answers { get; init; } = Answers ?? [];
    }
}
