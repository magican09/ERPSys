using System.Runtime.Serialization;
using MediatR;
using Catalog.gRPC.Application.Models;

namespace Catalog.gRPC.Application.Commands;
[DataContract]
public class CreateCatalogCommand:IRequest<int>
{
   [DataMember]
   public CatalogItemDTO CatalogItemDTO { get; init; }
    public CreateCatalogCommand(CatalogItemAppModel catalogItem)
    {
        CatalogItemDTO = new CatalogItemDTO
        {
            Name = catalogItem.Name,
            Description = catalogItem.Description,
            Synonym = catalogItem.Synonym,
            Autonumbering = catalogItem.Autonumbering,
            CheckUnique = catalogItem.CheckUnique,
            CodeLength = catalogItem.CodeLength,
            CodeType = catalogItem.CodeType,
            Code = catalogItem.Code,
            ChoiceMode = catalogItem.ChoiceMode,
            UseStandardCommands = catalogItem.UseStandardCommands,
            EditType = catalogItem.EditType,
            DefaultPresentation = catalogItem.DefaultPresentation,
            CodeAllowedLength = catalogItem.CodeAllowedLength,
            DescriptionLength = catalogItem.DescriptionLength,
            LevelCount = catalogItem.LevelCount,
            FoldersOnTop = catalogItem.FoldersOnTop,
            DataLockControlMode = catalogItem.DataLockControlMode,
            FullTextSearch = catalogItem.FullTextSearch,
            CreateOnInput = catalogItem.CreateOnInput,
            AttributeDescriptions = catalogItem.AttributeDescriotions
                .Select(ad=> new AttributeDescriptionDTO
                {
                    Id = ad.Id,
                    AttributeName = ad.AttributeName,
                    Description = ad.Description,
                    Synonym = ad.Synonym,
                    AttributeTypeName = ad.AttributeTypeName,
                    Properties = ad.Properties
                        .Select(p=>
                            new KeyValuePair<string,string>(p.Key,p.Value))
                        .ToDictionary(kvp=>kvp.Key,kvp=>kvp.Value)
                })
        };
    }
}

public record CatalogItemDTO
{
    public string Id { get; init; }
    public string Name{ get; init; }
    public  string Synonym  { get; init; }
    public  bool UseStandardCommands { get; init; }
    public string Code{ get; init; }
    public string Description { get; init; }
    public string CreateOnInput { get; init; }
    public string DataLockControlMode { get; init; }
    public string FullTextSearch { get; init; }
    public int LevelCount { get; init; }
    public bool FoldersOnTop { get; init; }
    public int CodeLength { get; init; }
    public int DescriptionLength { get; init; }
    public string CodeType  { get; init; }
    public int CodeAllowedLength { get; init; }
    public bool CheckUnique { get; init; }
    public bool Autonumbering { get; init; }
    public string DefaultPresentation { get; init; }
    public string EditType { get; init; }
    public string ChoiceMode { get; init; }
    public IEnumerable<AttributeDescriptionDTO> AttributeDescriptions { get; init; }
}

public record AttributeDescriptionDTO
{
    public string Id { get; init; }
    public string AttributeTypeName { get; init; }
    public string AttributeName { get; init; }
    public string Description { get; init; }
    public string Synonym{ get; init; }
    public Dictionary<string,string> Properties { get; init; }
   }

/*public record AttributeDTO
{
    public string Name { get; init; }
    public object? Value { get; init; }
    public Type AttributeValueType{ get; init; }
    public  Type Type{ get; init; }
}*/