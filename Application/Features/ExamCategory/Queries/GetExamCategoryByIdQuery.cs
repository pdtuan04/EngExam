using Application.Abstractions.Messaging;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Queries
{
    public sealed record GetExamCategoryByIdQuery(Guid Id) : ICacheQuery<ExamCategoryResponse>
    {
        public string CacheKey => $"examCategory_{Id}";

        public TimeSpan? Expiration => null;
    }
}
