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
        return new CatalogItemViewModel
        {
            CatalogId = catalog.Id.ToString(),
            Name = catalog.Name,
            Attributes = catalog.IntAttributeDescriptions.Select(at => new AttributeDescriptionViewModel
                {
                    AttributeDescriptionId = at.Id.ToString(),
                    AttributeName = at.AttributeName,
                    Description = at.Description,
                    Synonym = at.Synonym,
                    Type = at.Type.ToString(),
                    AttributeType = at.AttributeType.ToString(),
                }
            ).ToList()
        };
    }
}