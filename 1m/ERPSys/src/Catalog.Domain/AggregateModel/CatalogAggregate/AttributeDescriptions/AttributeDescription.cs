namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public  abstract class AttributeDescription<T>:Entity, IAttributeDescription where T : IAttribute
{
    public string AttributeName { get;  set; }
    public string? Description { get;  set; }
    public string? Synonym { get;  set; }
    public Type Type => this.GetType(); 
    public  Type AttributeType { get; internal set; } = typeof(T);

    public AttributeDescription()
    {
        
    }
    public AttributeDescription(IAttribute attribute)
    {
        
        AttributeName = attribute.Name; ;
      
    }

    public AttributeDescription(string attributeName)
    {
        AttributeName = attributeName;
    }
     
}