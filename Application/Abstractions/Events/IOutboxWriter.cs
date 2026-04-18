using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Events
{
    public interface IOutboxWriter
    {
        void Enqueue<T>(T @event) where T : IEventBus;
    }
}
