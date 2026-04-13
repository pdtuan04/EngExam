using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.ExamResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.ExamResult.Commands
{
    public sealed record SaveExamResultCommand(
        Guid UserId,
        Guid ExamId,
        IReadOnlyCollection<UserAnswerRequest> UserAnswers) : ICommand<ExamResultDetailResponse>;
}
