using Catalogs.Domain.AggregateModel.Common;

namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public abstract class Attribute<T>:Entity,IAttribute,IValueable<T>
{
    public string Name { get; internal set; }
    
    public T? Value { get; internal set; }
    public Type DescriptionType  { get; internal set; }
    public Type  ValueType { get; }
    public  Type Type => this.GetType();
    public Attribute()
    {
        
    }

    public Attribute(string name):this()
    {
        Name = name;
        
    }
    
}