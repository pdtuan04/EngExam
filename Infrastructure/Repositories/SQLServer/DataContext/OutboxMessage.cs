using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public sealed class OutboxMessage
    {
        public required Guid Id { get; init; }
        public required string Type { get; init; }
        public required string Payload { get; init; }
        public DateTime OccurredOn { get; init; }
        public DateTime? ProcessedOn { get; init; }
        public string? Error { get; init; }
    }
}
