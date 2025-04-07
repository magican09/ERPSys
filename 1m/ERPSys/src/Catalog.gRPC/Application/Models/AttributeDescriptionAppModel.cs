namespace Catalog.gRPC.Application.Models;

public class AttributeDescriptionAppModel
{
    public string Id { get; init; }
    public string CatalogItemId { get; init; }
    public string AttributeTypeName { get; init; }
    public string AttributeName { get; init; }
    public string Description { get; init; }
    public string Synonym{ get; init; }
    public Dictionary<string,string> Properties { get; init; }
}
 
