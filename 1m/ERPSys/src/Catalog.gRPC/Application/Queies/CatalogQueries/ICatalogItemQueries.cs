using Catalogs.Domain.AggregateModel.CatalogAggregate;

namespace Catalog.gRPC.Application.Queies;

public interface ICatalogItemQueries
{
    Task<CatalogItemViewModel> GetCatalogItemAsync(int id);
    
}