using Application.Models.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public sealed record UpdateExamRequest(
        Guid Id,
        bool IsActive,
        string Title,
        int DurationInMinutes,
        Guid ExamCategoryId,
        string? Description = null,
        string? ImageUrl = null,
        IReadOnlyCollection<UpdateQuestionRequest> Questions = null!)
    {
        public IReadOnlyCollection<UpdateQuestionRequest> Questions { get; init; } = Questions ?? [];
    }
}
