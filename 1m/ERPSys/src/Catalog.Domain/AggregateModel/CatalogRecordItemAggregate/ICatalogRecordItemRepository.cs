namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;

public interface ICatalogRecordItemRepository:IRepository<CatalogRecordItem>
{
   // IQueryable<CatalogRecordItem> GetAllAsync();
   CatalogRecordItem Add(CatalogRecordItem catalogRecordItem);
   Task<IEnumerable<CatalogRecordItem>> GetAllAsync();
}