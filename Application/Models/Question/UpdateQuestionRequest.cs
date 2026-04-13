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
    public record UpdateQuestionRequest(
    Guid Id,
    bool IsActive,
    string Content,
    double Score,
    QuestionTypes QuestionTypes,
    Guid TopicId,
    string? Explanation = null,
    string? ImageUrl = null,
    IReadOnlyCollection<UpdateAnswerRequest> Answers = null!)
    {
        public IReadOnlyCollection<UpdateAnswerRequest> Answers { get; init; } = Answers ?? [];
    }
}
