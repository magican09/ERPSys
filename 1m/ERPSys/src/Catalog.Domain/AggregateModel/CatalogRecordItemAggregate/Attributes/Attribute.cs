using Catalogs.Domain.AggregateModel.Common;
using Catalogs.Domain.Events;

namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public abstract class Attribute<T>:Entity,IAttribute,IValueable<T>
{
    public string Name { get; internal set; }
    
    public T? Value { get;private set;  }
    public Type DescriptionType  { get; internal set; }
    public void SetValue(object value)
    {
        Value = (T)value;
        this.AddDomainEvent(new AttributeValueChangeEvent(this));
    }

    public Type  ValueType => typeof(T);
    public  Type Type => this.GetType();
    public Attribute()
    {
        
    }

    public Attribute(string name):this()
    {
        Name = name;
        
    }
    
}