namespace Catalogs.Domain.Events;

public class AttributeDeletedEvent:INotification
{
    public IAttribute Attribute { get; }

    public AttributeDeletedEvent(IAttribute attribute)
    {
        Attribute = attribute;
    }
}