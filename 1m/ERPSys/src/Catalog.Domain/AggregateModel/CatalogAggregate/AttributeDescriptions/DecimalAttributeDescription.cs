namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public class DecimalAttributeDescription:AttributeDescription<DecimalAttribute>
{
    public DecimalAttributeDescription(){ }
    public DecimalAttributeDescription(IAttribute attribute):base(attribute) { }
}