using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache.CacheOptions
{
    public class CachableExamOption
    {
        public string CacheKey = "exam:";
        public TimeSpan CacheTimeSpan = TimeSpan.FromMinutes(10);
    }
}
