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
        
        builder.OwnsMany(o => o.IntAttributes,
            p =>
            {
                p.Ignore(o => o.DomainEvents);
              
                p.Ignore(o => o.Type);

                p.Ignore(o => o.ValueType);

                p.Property(o=>o.Id)
                    .UseHiLo("catalog_record_int_attribute_seq");
               
                p.Property(o => o.DescriptionType)
                    .HasConversion(
                        v=>v.ToString(),
                        v=>(Type) Enum.Parse(typeof(Type), v));

              
            }
                 );

      

    }
}