using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Handler
{
    public interface IOutboxWriter
    {
        Task AddOutboxMessage<T>(T message) where T : notnull;
    }
}
