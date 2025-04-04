using System.Data;
using Catalog.Infrastructure.Idempotency;
using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;


namespace Catalog.Infrastructure;

public class CatalogsContext:DbContext,IUnitOfWork
{
   public  DbSet<CatalogItem> Catalogs { get; set; }
   public DbSet<CatalogRecordItem> CatalogRecordItems { get; set; }
   
   //public DbSet<ClientRequest> ClientRequests { get; set; }
   
 
    
    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;
    public CatalogsContext()
    {
       
      
    }

    public CatalogsContext(DbContextOptions<CatalogsContext> options) : base(options)
    {
        
    }
    
    public CatalogsContext(DbContextOptions<CatalogsContext> options, IMediator mediator) : base(options)
    {
        try
        {
           Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        
        System.Diagnostics.Debug.WriteLine($"CatalogContext:: ctor ->{this.GetHashCode()}");
    }
    
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CatalogRecordItemEntityTypeConfiguration());
      
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {

        await _mediator.DispatchDomainEventAsinc(this);
        
     _ = await base.SaveChangesAsync(cancellationToken);
    
     return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;
        
        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
      
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction )
    {
        if(transaction==null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
    finally    
        {
            if (HasActiveTransaction)
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
    }
    
    
}
#nullable enable