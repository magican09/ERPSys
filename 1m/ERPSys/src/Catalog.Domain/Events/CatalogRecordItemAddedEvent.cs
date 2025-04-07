namespace Catalogs.Domain.Events;

public class CatalogRecordItemAddedEvent:INotification
{
    public int CatalogRecordId { get; set; }
    public CatalogRecordItemAddedEvent(int catalogRecordId)
    {
        CatalogRecordId = catalogRecordId;
    }
    
}