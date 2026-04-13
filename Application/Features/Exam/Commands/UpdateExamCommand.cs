using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.Exam.Commands
{
    public sealed record UpdateExamCommand(
        Guid Id,
        string Title,
        int DurationInMinutes,
        Guid ExamCategoryId,
        string? Description = null,
        bool? IsActive = null,
        IReadOnlyCollection<UpdateQuestionRequest> Questions = null!) : ICommand<ExamDetailResponse>;
}
