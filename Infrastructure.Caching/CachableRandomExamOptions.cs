using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Caching
{
    public class CachableRandomExamOptions
    {
        public string CacheKey { get; set; } = "RANDOM_EXAM:";
        public TimeSpan CacheTimeSpan { get; set; } = TimeSpan.FromSeconds(20);
    }
}
