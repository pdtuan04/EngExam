using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Queries
{
    public sealed record class GetAllCategoryQuery : ICacheQuery<List<ExamCategoryResponse>>
    {
        public string CacheKey => CacheKeys.AllExamCategories;

        public TimeSpan? Expiration => null;
    }
}
