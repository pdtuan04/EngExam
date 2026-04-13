using Application.Abstractions.Messaging;
using Application.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed record GetExamByIdQuery(Guid Id) : ICacheQuery<ExamResponse>
    {
        public string CacheKey => $"exam_{Id}";

        public TimeSpan? Expiration => null;
    }
}
