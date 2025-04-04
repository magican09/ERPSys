using Catalogs.Domain.AggregateModel.CatalogAggregate;

namespace Catalogs.Domain.Events;

public class CatalogItemCreatedEvent:INotification
{
    public CatalogItem CatalogItem { get;  }

    public CatalogItemCreatedEvent(CatalogItem catalogItem)
    {
        CatalogItem = catalogItem;
    }
}