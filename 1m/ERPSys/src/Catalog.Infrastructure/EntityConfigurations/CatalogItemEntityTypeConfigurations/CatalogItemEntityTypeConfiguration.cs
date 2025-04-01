using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration:IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");
        
        builder.Ignore(o=>o.DomainEvents);

        builder.Property(o => o.Id)
            .UseHiLo("catalog_seq");
        /*builder.OwnsMany(p => p.AttributeDescriptions,
            o =>
            {
                o.ToTable("AttributeDescriptions");
              
                o.Ignore(o => o.DomainEvents);
                
                o.Property(o=>o.Id)
                    .UseHiLo("attibute_description_seq");
                
                o.Property(o=>o.AttributeType)
                    .HasConversion(
                        v=>v.ToString(),
                        v=>(Type)Enum.Parse(typeof(Type), v));
                
                
            });*/
        
    }
}