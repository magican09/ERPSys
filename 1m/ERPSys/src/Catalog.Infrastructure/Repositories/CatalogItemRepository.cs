namespace Catalog.Infrastructure.Repositories;

public class CatalogItemRepository:ICatalogRepository
{
    private CatalogsContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public CatalogItemRepository(CatalogsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public CatalogItem Add(CatalogItem catalogItem)
    {
        return _context.Catalogs.Add(catalogItem).Entity;
    }

    public void Update(CatalogItem catalogItem)
    {
        _context.Entry(catalogItem).State = EntityState.Modified;
    }

    public async Task<CatalogItem> GetAsync(int catalogItemId)
    {
        var catalogItem = await _context.Catalogs.FindAsync(catalogItemId);
        /*if (catalogItemId != null) это судя по всему не нужно, потому что свойства аттрибутов у нсс OwenMany для каталога
        {
            await _context.Entry(catalogItem)
                .Collection(i=>i.IntAttributeDescriptions).LoadAsync();
        }*/
        return catalogItem;
    }

    public async Task<CatalogItem> GetByNameAsync(string catalogName)
    {
        var catalogItem =await _context.Catalogs.Where(c=>c.Name==catalogName).FirstOrDefaultAsync();
      
        return catalogItem;
    }
}