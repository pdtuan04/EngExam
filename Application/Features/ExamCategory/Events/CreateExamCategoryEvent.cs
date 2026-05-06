using Application.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Events
{
    public sealed record CreateExamCategoryEvent(
        Guid CategoryId,
        string Name,
        string Description,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        string? ImageUrl = null,
        bool IsActive = default
        );
}
