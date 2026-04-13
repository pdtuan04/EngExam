using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Commands
{
    public sealed record AddExamCommand(string Title,
    int DurationInMinutes,
    Guid ExamCategoryId,
    string? Description = null,
    IReadOnlyCollection<CreateQuestionRequest> Questions = null!) : ICommand<ExamDetailResponse>;
}
