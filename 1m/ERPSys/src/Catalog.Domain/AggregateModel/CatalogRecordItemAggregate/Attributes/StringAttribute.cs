namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public class StringAttribute:Attribute<string>
{
    public StringAttribute()
    {
        DescriptionType = typeof(StringAttributeDescription);
    }
}