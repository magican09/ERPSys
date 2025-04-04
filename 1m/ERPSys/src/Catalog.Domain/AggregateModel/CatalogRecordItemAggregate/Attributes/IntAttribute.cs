namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public class IntAttribute:Attribute<int>
{
    

    public IntAttribute(string name):base(name)
    {
        DescriptionType = typeof(IntAttributeDescription);
    }   
}