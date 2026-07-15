using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed record GetCompletedExamCountByMonthQuery(int Year, int Month) : IQuery<int>;
}
