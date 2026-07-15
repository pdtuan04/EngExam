using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed record GetExamByKeyWordQuery(string keyWord) : ICacheQuery<IEnumerable<ExamSuggestResponse>>
    {
        public string CacheKey => CacheKeys.ExamSuggested(keyWord);
        public TimeSpan? Expiration => null;
    }
}
