namespace Catalogs.Domain.Events;

public class AttributeNameChagedEvent:INotification
{
    public IAttribute Attribute { get; }

    public AttributeNameChagedEvent(IAttribute attribute)
    {
        Attribute = attribute;
    }
}