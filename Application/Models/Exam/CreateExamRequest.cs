using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public record CreateExamRequest(
        string Title,
        int DurationInMinutes,
        Guid ExamCategoryId,
        string? Description = null,
        IReadOnlyCollection<CreateQuestionRequest> Questions = null!,
        IReadOnlyCollection<CreateQuestionGroupRequest>? QuestionGroups = null)
    {
        public IReadOnlyCollection<CreateQuestionRequest> Questions { get; init; } = Questions ?? [];
        public IReadOnlyCollection<CreateQuestionGroupRequest> QuestionGroups { get; init; } = QuestionGroups ?? [];
    }
}
