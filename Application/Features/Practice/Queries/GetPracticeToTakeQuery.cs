using Application.Abstractions.Messaging;
using Application.Models.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Queries
{
    public sealed record GetPracticeToTakeQuery(Guid Id) : ICacheQuery<DoPracticeResponse>
    {
        public string CacheKey => $"practice_{Id}";

        public TimeSpan? Expiration => null ;
    }
}
