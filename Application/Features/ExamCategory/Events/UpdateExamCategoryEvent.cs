using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Events
{
    public sealed record UpdateExamCategoryEvent(Guid CategoryId,
        string Name,
        string Description,
        DateTime UpdatedAt,
        DateTime CreatedAt,
        string? ImageUrl = null,
        bool IsActive = default);
}
