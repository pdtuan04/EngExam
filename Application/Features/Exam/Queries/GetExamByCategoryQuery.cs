using Application.Abstractions.Messaging;
using Application.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed record GetExamByCategoryQuery(Guid CategoryId) : ICacheQuery<IEnumerable<ExamResponse>>
    {
        public string CacheKey => $"examByCategory_{CategoryId}";

        public TimeSpan? Expiration => null;
    }
}
