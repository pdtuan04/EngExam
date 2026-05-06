using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Events
{
    public sealed record DeletedExamCategoryEvent(Guid Id, DateTime DeletedAt);
}
