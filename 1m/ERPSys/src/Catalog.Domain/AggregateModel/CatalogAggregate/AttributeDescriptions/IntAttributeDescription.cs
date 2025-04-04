namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public class IntAttributeDescription:AttributeDescription<IntAttribute>
{ 
    public int MinValue { get; set; }   = Int32.MinValue;
    public int MaxValue { get; set; }   = Int32.MaxValue;
    public bool NonNegative { get; set; }
    public IntAttributeDescription(){ }
    public IntAttributeDescription(IAttribute attribute):base(attribute) { }
    
}