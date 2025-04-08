namespace Catalogs.Domain.Events.AttributeDescriptionEvents;

public class AttributeDescriptionPropertiesChangedEvent
{
    public IAttributeDescription AttributeDescription { get; set; }

    public AttributeDescriptionPropertiesChangedEvent(IAttributeDescription attributeDescription)
    {
        AttributeDescription = attributeDescription;
    }
}