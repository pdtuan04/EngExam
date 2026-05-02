using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.Practice;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.Practice.Commands
{
    public sealed record UpdatePracticeCommand(
        Guid Id,
        string Title,
        Guid TopicId,
        string? Description = null,
        bool? IsActive = null,
        IReadOnlyCollection<UpdateQuestionRequest> Questions = null!) : ICommand<PracticeDetailResponse>;
}
