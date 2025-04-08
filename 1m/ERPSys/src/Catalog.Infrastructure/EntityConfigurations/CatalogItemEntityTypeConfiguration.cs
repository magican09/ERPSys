using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;


public class CatalogItemEntityTypeConfiguration:IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");
        
        builder.Ignore(o=>o.DomainEvents);
        builder.Ignore(o=>o.AttributeDescriptionsMap);

  
           

        builder.Property(o => o.Id)
            .UseHiLo("catalog_seq");
       
        builder.OwnsMany(p => p.IntAttributeDescriptions,
            o =>
            {
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
            
                o.Property(o=>o.Id)
                    .UseHiLo("int_attribute_description_seq");
             
                o.Property(o=>o.AttributeType)
                    .HasConversion<StringToTypeConverter>();
                
            });
    
        builder.OwnsMany(p => p.DecimalAttributeDescriptions,
            o =>
            {
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
            
                o.Property(o=>o.Id)
                    .UseHiLo("dec_attribute_description_seq");
             
                o.Property(o=>o.AttributeType)
                    .HasConversion<StringToTypeConverter>();
                
            });
        builder.OwnsMany(p => p.StringAttributeDescriptions,
            o =>
            {
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
            
                o.Property(o=>o.Id)
                    .UseHiLo("str_attribute_description_seq");
             
                o.Property(o=>o.AttributeType)
                    .HasConversion<StringToTypeConverter>();
                
            });
        
        builder.OwnsMany(p => p.CatalogRecordItemAttributeDescriptions,
            o =>
            {
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
            
                o.Property(o=>o.Id)
                    .UseHiLo("catrec_attribute_description_seq");
             
                o.Property(o=>o.AttributeType)
                    .HasConversion<StringToTypeConverter>();
                
            });
    }
}