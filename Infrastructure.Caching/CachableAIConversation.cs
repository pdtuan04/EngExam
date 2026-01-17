using Application.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Caching
{
    public class CachableAIConversation
    {
        private readonly CachableAIConversationOption _options;
        private IDistributedCache _cache;
        private DistributedCacheEntryOptions _cacheEntryOptions;
        private readonly IAIChatBox _aiChatBox;
        private ILogger<CachableAIConversation> _logger;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
        };
        public CachableAIConversation(IDistributedCache cache, CachableAIConversationOption options, IAIChatBox aIChatBox, ILogger<CachableAIConversation> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _aiChatBox = aIChatBox ?? throw new ArgumentNullException(nameof(aIChatBox));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = options.Expiration
            };
        }
        public async Task<string> GetAIConversationAsync(Guid id,string prompt)
        {
            var cacheKey = $"{_options.CacheKey}#{id}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                _logger.LogInformation($"Cache miss for key: {cacheKey}");
                var response = await _aiChatBox.GetAIResponseAsync(prompt);
                cachedData = JsonSerializer.Serialize(response, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, cachedData, _cacheEntryOptions);
                return response;
            }
            else
            {
                var response = JsonSerializer.Deserialize<string>(cachedData);
                return response;
            }
        }
    }
}
