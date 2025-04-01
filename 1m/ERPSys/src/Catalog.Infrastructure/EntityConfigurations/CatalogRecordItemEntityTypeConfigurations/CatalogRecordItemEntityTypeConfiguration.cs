using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class CatalogRecordItemEntityTypeConfiguration: IEntityTypeConfiguration<CatalogRecordItem>
{
    public void Configure(EntityTypeBuilder<CatalogRecordItem> builder)
    {
        builder.ToTable("CatalogRecordItem");
        builder.Ignore(o => o.DomainEvents);
        builder.Property(o => o.Id)
            .UseHiLo("catalog_record_item_seq");
       
        
    }
}