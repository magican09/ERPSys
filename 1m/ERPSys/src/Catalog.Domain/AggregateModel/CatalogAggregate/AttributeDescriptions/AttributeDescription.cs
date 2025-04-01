namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public  abstract class AttributeDescription:Entity,IAttributeDescription
{
    public string Name { get; internal set; }
    public string? Description { get; internal set; }
    public string? Synonym { get; internal set; }
    public  Type AttributeType { get; internal set; }

    public AttributeDescription()
    {
        
    }
    public AttributeDescription(Type attributeType ):this()
    {
        AttributeType = attributeType;
    }
  //  public Type ValueType { get; internal set; }
}