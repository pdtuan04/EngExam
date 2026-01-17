using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Caching
{
    public class CachableAIConversationOption
    {
        public string CacheKey { get; set; } = "AIConversation";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(10);
    }
}
