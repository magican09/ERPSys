namespace Catalogs.Domain.Events.AttributeDescriptionEvents;

public class AttributeDescriptionAddedEvent:INotification
{
    public IAttributeDescription AttributeDescription { get; }

    public AttributeDescriptionAddedEvent(IAttributeDescription attributeDescription)
    {
        AttributeDescription = attributeDescription;
    }
    
}