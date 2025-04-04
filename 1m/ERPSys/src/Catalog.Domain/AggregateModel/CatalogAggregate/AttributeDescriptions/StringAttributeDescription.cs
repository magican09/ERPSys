namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public class StringAttributeDescription:AttributeDescription<StringAttribute>
{
    public StringAttributeDescription(){ }
    public StringAttributeDescription(IAttribute attribute):base(attribute) { }
}