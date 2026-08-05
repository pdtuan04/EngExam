using Application.Common.Caching;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Realtime
{
    /// <summary>
    /// can use simple in-memory counter, 
    /// but it can't be shared across multiple instances of the application
    /// </summary>
    public class OnlineCounterHub : Hub
    {
        private readonly IDatabase _db;
        private readonly ILogger<OnlineCounterHub> _logger;
        public OnlineCounterHub(IConnectionMultiplexer redis, ILogger<OnlineCounterHub> logger)
        {
            _db = redis.GetDatabase();
            _logger = logger;
        }
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            await _db.SetAddAsync(CacheKeys.OnlineUsers, connectionId);
            var count = await _db.SetLengthAsync(CacheKeys.OnlineUsers);
            await Clients.All.SendAsync("Online", count);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            await _db.SetRemoveAsync(CacheKeys.OnlineUsers, connectionId);

            var count = await _db.SetLengthAsync(CacheKeys.OnlineUsers);

            await Clients.All.SendAsync("Online", count);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
