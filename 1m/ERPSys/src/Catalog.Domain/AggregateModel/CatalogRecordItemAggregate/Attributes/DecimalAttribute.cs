namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public class DecimalAttribute:Attribute<decimal>
{
    public DecimalAttribute()
    {
        DescriptionType = typeof(DecimalAttributeDescription);
    }
}