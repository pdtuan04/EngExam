using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Caching
{
    public class CachableExamFinderOptions
    {
        public string CacheKey { get; set; } = "Exam|";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromSeconds(20);
    }
}
