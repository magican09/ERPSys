namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;

public interface ICatalogRecordItemRepository
{
   // IQueryable<CatalogRecordItem> GetAllAsync();
   Task<IEnumerable<CatalogRecordItem>> GetAllAsync();
}