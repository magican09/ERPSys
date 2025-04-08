using Catalog.gRPC.Application.Queies;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;
using Catalogs.Domain.Events.AttributeDescriptionEvents;
using MediatR;

namespace Catalog.gRPC.Application.DomainEventHandlers.AttributeEventHandlers;

public class AttributeDescriptionAddedEventHandler:INotificationHandler<AttributeDescriptionAddedEvent>
{
    private readonly ILogger<AttributeDescriptionAddedEventHandler> _logger;
    private readonly ICatalogRecordItemRepository _catalogRecords;
    
    public AttributeDescriptionAddedEventHandler(
        ICatalogRecordItemRepository catalogRecordItemRepository,
        ILogger<AttributeDescriptionAddedEventHandler> logger)
    {
        _logger = logger;
        _catalogRecords = catalogRecordItemRepository;
    }


    public async Task Handle(AttributeDescriptionAddedEvent notification, CancellationToken cancellationToken)
    {

       var  allRecords =  await _catalogRecords.GetAllAsync();

       var attribuDescription = notification.AttributeDescription;
       
       var atributeTyp = attribuDescription.AttributeType;

       var attributeName = attribuDescription.AttributeName;
       
       foreach (var record in allRecords)
       {
           record.CreateAttribute(atributeTyp, attributeName,record.GetDefault(atributeTyp));
       }
       
    }
}