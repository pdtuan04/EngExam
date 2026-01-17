using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Caching
{
    public class CachableExamByIdOptions
    {
        public string CacheKey { get; set; } = "Exam|Id";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromSeconds(10);
    }
}
