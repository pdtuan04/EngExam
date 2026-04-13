using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public sealed record ExamResponse(
    Guid Id,
    string Title,
    string? Description,
    int DurationInMinutes,
    Guid ExamCategoryId,
    DateTime CreatedAt);
}
