namespace Catalog.gRPC.Application.Queies;

public record CatalogItemViewModel
{  
    public string Id { get; init; }
    public string Name { get; init; }
    public string? Synonym  { get; init; }
    public  bool UseStandardCommands  { get; init; }
    public string? Code  { get; init; }
    public string? Description  { get; init; }
    public string? CreateOnInput  { get; init; }
    public string? DataLockControlMode { get; init; }
    public string? FullTextSearch { get; init; }
    public int LevelCount  { get; init; }
    public bool FoldersOnTop  { get; init; }
    public int CodeLength  { get; init; }
    public int DescriptionLength  { get; init; }
    public string? CodeType   { get; init; }
    public int CodeAllowedLength  { get; init; }
    public bool CheckUnique  { get; init; }
    public bool Autonumbering { get; init; }
    public string? DefaultPresentation  { get; init; }
    public string? EditType  { get; init; }
    public string? ChoiceMode { get; init; }
    public List<AttributeDescriptionViewModel> AttributeDescriptions { get; init; }
    
}

public  record AttributeDescriptionViewModel
{
    public string AttributeDescriptionId { get; init; } 
    public string AttributeName { get; init; }
    public string Description { get; init; }
    public string Synonym { get; init; }
    public string Type { get; init; }
    public  string AttributeType  { get; init; }
    
    public Dictionary<string,(string,string)> Properties { get; init; } //Dictionary<PropName,(PropValueType,{PropValue.ToString()>
} 
