using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Events
{
    public record CreateExamResultEvent(Guid Id,
        string Title,
        string? Description,
        int DurationInMinutes,
        DateTime CompleteAt,
        double Score,
        Guid ExamId,
        Guid UserId);
}
