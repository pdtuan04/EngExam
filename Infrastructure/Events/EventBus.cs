using Application.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    public sealed class EventBus(InmemoryEventBus queue) : IEventBus
    {
        public async Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default) where TEvent : IIntegationEvent
        {
            await queue.Writer.WriteAsync(integrationEvent, cancellationToken);
        }
    }
}
