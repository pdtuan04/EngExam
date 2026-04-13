using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public sealed record SubmitExamRequest(
    Guid ExamId,
    IReadOnlyCollection<UserAnswerRequest> UserAnswers)
    {
        public IReadOnlyCollection<UserAnswerRequest> UserAnswers { get; init; } = UserAnswers ?? [];
    }
    public sealed record UserAnswerRequest(
    Guid QuestionId,
    Guid? AnswerId,
    string? AnswerFillInBlank);
}
