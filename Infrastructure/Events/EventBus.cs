using Application.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    internal sealed class EventBus : IEventBus
    {
        public async Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default) where TEvent : IIntegationEvent
        {
            throw new NotImplementedException();
        }
    }
}
