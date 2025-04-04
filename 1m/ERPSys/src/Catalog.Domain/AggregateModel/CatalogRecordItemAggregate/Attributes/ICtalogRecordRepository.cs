

namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

public interface ICtalogRecordRepository: IRepository<CatalogRecordItem>
{
    IQueryable<CatalogRecordItem> GetAllAsync();
    
}