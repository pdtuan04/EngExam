using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    public sealed class IntegrationEventProcessJob(
        InmemoryEventBus queue, 
        IPublisher publisher,
        ILogger<IntegrationEventProcessJob> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var integrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    logger.LogInformation("Processing integration event: {EventType}", integrationEvent.Id);
                    await publisher.Publish(integrationEvent, stoppingToken);
                    logger.LogInformation("Successfully processed integration event: {EventType}", integrationEvent.Id);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing integration event: {EventType}", integrationEvent.GetType().Name);
                }
            }
        }
    }
}
