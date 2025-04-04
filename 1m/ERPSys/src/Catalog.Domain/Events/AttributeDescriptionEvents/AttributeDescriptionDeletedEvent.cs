namespace Catalogs.Domain.Events.AttributeDescriptionEvents;

public class AttributeDescriptionDeletedEvent:INotification
{
    public IAttributeDescription AttributeDescription { get; }

    public AttributeDescriptionDeletedEvent(IAttributeDescription attributeDescription)
    {
        AttributeDescription = attributeDescription;
    }
}