namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public class CatalogRecordItemAttributeDescription:AttributeDescription<CatalogRecordItemAttribute>
{
    public CatalogRecordItemAttributeDescription(){ }
    public CatalogRecordItemAttributeDescription(IAttribute attribute):base(attribute) { }
}