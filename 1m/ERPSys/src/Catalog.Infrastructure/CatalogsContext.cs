using System.Data;
using Catalog.Infrastructure.EntityConfigurations.AtributeEntityTypeConfigurations;
using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;


namespace Catalog.Infrastructure;

public class CatalogsContext:DbContext,IUnitOfWork
{
    DbSet<CatalogItem> Catalogs { get; set; }
    DbSet<CatalogRecordItem> CatalogRecordItems { get; set; }
    
    DbSet<AttributeDescription> AttributeDescriptions { get; set; }
    DbSet<IntAttributeDescription> IntAttributeDescriptions { get; set; }
    DbSet<StringAttributeDescription> StringAttributeDescriptions { get; set; }
    DbSet<DecimalAttributeDescription> DecimalAttributeDescriptions { get; set; }
    DbSet<CatalogRecordItemAttributeDescription> CatalogRecordItemAttributeDescriptions { get; set; }

    
  //  DbSet<CatalogRecordItem> CatalogRecords { get; set; }
    
    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;

    public CatalogsContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public CatalogsContext(DbContextOptions<CatalogsContext> options) : base(options){}
    
    public CatalogsContext(DbContextOptions<CatalogsContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        
        System.Diagnostics.Debug.WriteLine($"CatalogContext:: ctor ->{this.GetHashCode()}");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;TrustServerCertificate=True;Database=dict_db;User=sa;MultipleActiveResultSets = Yes; Password=aA26497852)");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeDescriptionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeEntityTypeConfiguration());

        //   modelBuilder.ApplyConfiguration(new IntAttributeDescriptionEntityTypeConfiguration());
    }

    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
#nullable enable