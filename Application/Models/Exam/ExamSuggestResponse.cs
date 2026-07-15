using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public sealed record ExamSuggestResponse(Guid Id, string Title, string? Description);
}
