namespace Catalogs.Domain.AggregateModel.CatalogAggregate;

public class CatalogItem:Entity,IAggregateRoot
{
   
    public string Name { get; private set; }
    public string? Code { get; private set; }
    public string? Descriptions { get; private set; }
    
    private List<AttributeDescription> _attributeDescriptions;
    public IReadOnlyCollection<AttributeDescription> AttributeDescriptions => _attributeDescriptions.AsReadOnly();
  

    protected CatalogItem()
    {
        _attributeDescriptions=new List<AttributeDescription>();
    }

    public CatalogItem(string name):this()
    {
        Name = name;
    }

    public void AddAttributeDescription(string attributeName, Type attributeType)
    {
        
    }
}