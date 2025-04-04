namespace Catalog.gRPC.Application.Queies;

public record CatalogItemViewModel
{
    public string CatalogId { get; init; }
    public string Name { get; init; }
    public List<AttributeDescriptionViewModel> Attributes { get; init; }
    
}

public  record AttributeDescriptionViewModel
{
    public string AttributeDescriptionId { get; init; } 
    public string AttributeName { get; init; }
    public string Description { get; init; }
    public string Synonym { get; init; }
    public string Type { get; init; }
    public  string AttributeType  { get; init; }
} 
