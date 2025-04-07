using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;
using Catalogs.Domain.Events.AttributeDescriptionEvents;
using MediatR;

namespace Catalog.gRPC.Application.DomainEventHandlers.AttributeEventHandlers;

public class AttributeDescriptionDeletedEventHandler:INotificationHandler<AttributeDescriptionDeletedEvent>
{
private readonly ILogger<AttributeDescriptionDeletedEventHandler> _logger;
private readonly ICatalogRecordItemRepository _catalogRecords;
    
public AttributeDescriptionDeletedEventHandler(
    ICatalogRecordItemRepository catalogRecordItemRepository,
    ILogger<AttributeDescriptionDeletedEventHandler> logger)
{
    _logger = logger;
    _catalogRecords = catalogRecordItemRepository;
}


public async Task Handle(AttributeDescriptionDeletedEvent notification, CancellationToken cancellationToken)
{

    var  allRecords =  await _catalogRecords.GetAllAsync();
       
    var attribute = (IAttribute)Activator.CreateInstance(notification.AttributeDescription.AttributeType,
        new object[] { notification.AttributeDescription.AttributeName } );

    foreach (var record in allRecords)
    {
        record.DeleteAttribute(attribute);
    }
       
}
}