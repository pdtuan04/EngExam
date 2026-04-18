using Application.Abstractions.Messaging;
using Application.Models.ExamResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed record GetExamResultDetailsQuery(Guid Id) : ICacheQuery<ExamResultDetailResponse>
    {
        public string CacheKey => $"exam_result_details_{Id}";

        public TimeSpan? Expiration => null;
    }
}
