using Catalogs.Domain.Seedwork;

namespace Catalogs.Domain.AggregateModel.CatalogAggregate;

public interface ICatalogRepository:IRepository<CatalogItem>
{
    CatalogItem Add(CatalogItem catalogItem);

    void Update(CatalogItem catalogItem);

    Task<CatalogItem> GetAsync(int catalogItemId);
    Task<CatalogItem> GetByNameAsync(string catalogName);
}