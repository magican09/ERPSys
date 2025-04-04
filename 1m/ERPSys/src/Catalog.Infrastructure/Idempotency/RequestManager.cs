using Catalogs.Domain.Exceptions;

namespace Catalog.Infrastructure.Idempotency;

public class RequestManager:IRequestManager
{
    private readonly CatalogsContext _context;
    
    
    public RequestManager(CatalogsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<bool> ExistAsync(Guid id)
    {
        try
        {
            var request = await _context
                .FindAsync<ClientRequest>(id);
            return request != null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
      
    }

    public  async Task CreateRequestForCommandAsync<T>(Guid id)
    {
       var exists = await ExistAsync(id);
       var request = exists ? 
           throw new CatalogDomainException($"Request with {id} already exists") :
           new ClientRequest()
           {
               Id = id,
               Name = typeof(T).Name,
               Time = DateTime.UtcNow
           };
       _context.Add(request);
       await _context.SaveChangesAsync();
    }
}