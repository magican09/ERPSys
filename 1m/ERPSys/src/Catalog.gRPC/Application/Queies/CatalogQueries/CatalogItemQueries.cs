using Catalog.Infrastructure;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Microsoft.EntityFrameworkCore;

namespace Catalog.gRPC.Application.Queies;

public class CatalogItemQueries(CatalogsContext context) :ICatalogItemQueries
{
    
    public async Task<CatalogItemViewModel> GetCatalogItemAsync(int id)
    {
      
        var catalog = await context.Catalogs.FirstOrDefaultAsync(c => c.Id == id);
    
        if (catalog is null)
            throw new KeyNotFoundException();
   
        var allAttributeDescrtiptions = catalog.GetAllAttributeDescriptionsFields();
        
        return new CatalogItemViewModel
        {   Id = catalog.Id.ToString(),
            Name = catalog .Name,
            Description = catalog.Description,
            Synonym = catalog.Synonym,
            Autonumbering = catalog.Autonumbering,
            CheckUnique = catalog.CheckUnique,
            CodeLength = catalog.CodeLength,
            CodeType = catalog.CodeType,
            Code = catalog.Code,
            ChoiceMode = catalog.ChoiceMode,
            UseStandardCommands = catalog.UseStandardCommands,
            EditType = catalog.EditType,
            DefaultPresentation = catalog.DefaultPresentation,
            CodeAllowedLength = catalog.CodeAllowedLength,
            DescriptionLength = catalog.DescriptionLength,
            LevelCount = catalog.LevelCount,
            FoldersOnTop = catalog.FoldersOnTop,
            DataLockControlMode = catalog.DataLockControlMode,
            FullTextSearch = catalog.FullTextSearch,
            CreateOnInput = catalog.CreateOnInput,
            AttributeDescriptions= catalog.GetAllAttributeDescriptionsFields()
                .Select(adf =>
                    new AttributeDescriptionViewModel
                    {
                        Type = adf.Item1.ToString(),
                        
                    }
               
            ).ToList()
        };
    }
}