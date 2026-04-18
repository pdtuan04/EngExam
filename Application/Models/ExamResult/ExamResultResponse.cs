using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamResult
{
    public sealed record ExamResultResponse(
        Guid Id,
        DateTime CompleteAt,
        double Score);
}
