namespace Catalogs.Domain.Events.AttributeDescriptionEvents;

public class AttributeDescriptionUpdatedEvent:INotification
{
    public IAttributeDescription AttributeDescription { get;  }

    public AttributeDescriptionUpdatedEvent(IAttributeDescription attributeDescription)
    {
        AttributeDescription = attributeDescription;
    }
}