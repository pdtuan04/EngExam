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
    public class OnlineCounter : Hub
    {
        private readonly IDatabase _db;
        private readonly ILogger<OnlineCounter> _logger;
        public OnlineCounter(IConnectionMultiplexer redis, ILogger<OnlineCounter> logger)
        {
            _db = redis.GetDatabase();
            _logger = logger;
        }
        public override async Task OnConnectedAsync()
        {

            var connectionId = Context.ConnectionId;
            await _db.SetAddAsync("online:all", connectionId);
            var count = await _db.SetLengthAsync("online:all");
            await Clients.All.SendAsync("Online", count);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            await _db.SetRemoveAsync("online:all", connectionId);

            var count = await _db.SetLengthAsync("online:all");

            await Clients.All.SendAsync("Online", count);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
