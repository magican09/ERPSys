using Catalogs.Domain.Events;
using MediatR;

namespace Catalog.gRPC.Application.DomainEventHandlers;

public class CatalogItemCreatedEventHandler:INotificationHandler<CatalogItemCreatedEvent>
{
    private readonly ILogger<CatalogItemCreatedEventHandler> _logger;
    public CatalogItemCreatedEventHandler(ILogger<CatalogItemCreatedEventHandler> logger)
    {
      _logger = logger;
    }
    public Task Handle(CatalogItemCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
       _logger.LogInformation($"Catalog {domainEvent.CatalogItem.Name} created");
       return Task.CompletedTask;
    }
}