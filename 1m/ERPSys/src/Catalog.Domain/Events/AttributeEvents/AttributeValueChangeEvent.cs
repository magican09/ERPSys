namespace Catalogs.Domain.Events;

public class AttributeValueChangeEvent:INotification
{
    public IAttribute Attribute { get; }
    public AttributeValueChangeEvent(IAttribute attribute)
    {
        Attribute = attribute;
    }
    
}