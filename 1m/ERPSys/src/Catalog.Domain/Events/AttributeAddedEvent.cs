namespace Catalogs.Domain.Events;

public class AttributeAddedEvent:INotification
{
    public IAttribute Attribute { get; }

    public AttributeAddedEvent(IAttribute attribute)
    {
        Attribute = attribute;
    }
    
}