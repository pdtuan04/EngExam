using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamCategory
{
    public record ExamCategoryReadModel(
        Guid Id,
        string Name,
        string Description,
        string? ImageUrl,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsDeleted = false
    );
}
