namespace Catalog.Infrastructure.Repositories;

public class CatalogRecordItemRepository:ICatalogRecordItemRepository
{
    private CatalogsContext _context;
    
    public IUnitOfWork UnitOfWork => _context;
    
    public CatalogRecordItemRepository(CatalogsContext context)
    {
        _context = context;
    }


    public CatalogRecordItem Add(CatalogRecordItem catalogRecordItem)
    {
        return _context.Add(catalogRecordItem).Entity;
    }

    public async Task<IEnumerable<CatalogRecordItem>> GetAllAsync()
    {
        return await _context.CatalogRecordItems.ToListAsync();
    }
}