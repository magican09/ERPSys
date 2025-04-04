
namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public class CatalogRecordItemAttribute:Attribute<CatalogRecordItem>
{
    public CatalogRecordItemAttribute()
    {
        DescriptionType = typeof(CatalogRecordItemAttributeDescription);
    }

}