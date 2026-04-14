using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamCategory
{
    public sealed record ExamCategoryResponse(
    Guid Id,
    string Name,
    string? Description,
    string? ImageUrl);
}