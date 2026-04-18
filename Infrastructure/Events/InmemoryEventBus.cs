using Application.Abstractions.Events;
using System.Threading.Channels;

namespace Infrastructure.Events
{
    public sealed class InmemoryEventBus
    {
        private readonly Channel<IIntegationEvent> _channel = Channel.CreateUnbounded<IIntegationEvent>();
        public ChannelWriter<IIntegationEvent> Writer => _channel.Writer;
        public ChannelReader<IIntegationEvent> Reader => _channel.Reader;

    }
}
