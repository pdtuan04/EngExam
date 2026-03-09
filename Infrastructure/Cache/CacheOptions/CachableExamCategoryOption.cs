using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache.CacheOptions
{
    public class CachableExamCategoryOption
    {
        public string CacheKey = "examcategory:";
        public TimeSpan CacheTimeSpan = TimeSpan.FromMinutes(10);
    }
}
