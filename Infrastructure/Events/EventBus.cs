using Application.Abstractions.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
