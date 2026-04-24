using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.Practice;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Commands
{
    public sealed record AddPracticeCommand(
        string Title,
        Guid TopicId,
        string? Description = null, 
        IReadOnlyCollection<CreateQuestionRequest> Questions = null!) : ICommand<PracticeDetailResponse>;
}
