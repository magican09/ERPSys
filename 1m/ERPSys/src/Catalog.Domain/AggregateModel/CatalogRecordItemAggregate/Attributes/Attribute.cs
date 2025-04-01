namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public abstract class Attribute<T>:Entity,IAttribute
{
    public string Name { get; internal set; }
    public  Type Type => this.GetType();
    public Type ValueType => typeof(T);
    public T? Value { get; internal set; }
    
    
    
}