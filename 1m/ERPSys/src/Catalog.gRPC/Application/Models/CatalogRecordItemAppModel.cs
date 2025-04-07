namespace Catalog.gRPC.Application.Models;

public class CatalogRecordItemAppModel
{
    
    public string Id { get; init;}
    public string CatalogItemId { get; init;}
    
    public List<AttributeAppModel> Attributes { get; init;}
    
}