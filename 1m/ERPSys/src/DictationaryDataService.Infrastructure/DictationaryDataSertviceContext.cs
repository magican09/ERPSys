



using DictationaryDataService.Domain.SeedWork;
using DictationaryDataService.Infrastructure.EntityConfigurations;

namespace DictationaryDataService.Infrastructure;

public class DictationaryDataSertviceContext:DbContext,IUnitOfWork
{
    public DbSet<DictationaryData> DictationaryDatas { get; set; }
    public DbSet<DictationaryDataItem>  DictationaryDataItems { get; set; }
 
    private readonly IMediator _mediator;
    
    private IDbContextTransaction _currentTransaction;
  
    public bool HasActiveTransaction => _currentTransaction != null;
    public DictationaryDataSertviceContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();     
    }
    
    public DictationaryDataSertviceContext(DbContextOptions<DictationaryDataSertviceContext> options) : base(options) { }
    public DictationaryDataSertviceContext(DbContextOptions<DictationaryDataSertviceContext> options, IMediator mediator)
    {
        _mediator = mediator??throw new ArgumentNullException(nameof(mediator));
            
        System.Diagnostics.Debug.WriteLine("DataDictationaryContext::ctor ->" + this.GetHashCode());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;TrustServerCertificate=True;Database=dict_db;User=sa;MultipleActiveResultSets = Yes; Password=aA26497852)");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DictationaryDataEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DictationaryDataItemEntityTypeConfiguration());
     //   modelBuilder.ApplyConfiguration(new RequisitePropertyEntityTypeConfiguration());
    }

    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
#nullable  enable