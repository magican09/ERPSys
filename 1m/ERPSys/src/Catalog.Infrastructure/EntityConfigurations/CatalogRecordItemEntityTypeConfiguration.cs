using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class CatalogRecordItemEntityTypeConfiguration: IEntityTypeConfiguration<CatalogRecordItem>
{
    public void Configure(EntityTypeBuilder<CatalogRecordItem> builder)
    {
        builder.ToTable("CatalogRecordItem");
      
        builder.Ignore(o => o.DomainEvents);
        
        builder.Ignore(o => o.AttributesMap);
        
        builder.Property(o => o.Id)
            .UseHiLo("catalog_record_item_seq");
        
        builder.OwnsMany(o => o.IntAttributes,
            p =>
            {
                p.Ignore(o => o.DomainEvents);
              
                p.Ignore(o => o.Type);

                p.Ignore(o => o.ValueType);

                p.Property(o=>o.Id)
                    .UseHiLo("int_record_attribute_seq");
               
                p.Property(o => o.DescriptionType)
                    .HasConversion<StringToTypeConverter>();
            });
      
        builder.OwnsMany(o => o.DecimalAttributes,
            p =>
            {
                p.Ignore(o => o.DomainEvents);
              
                p.Ignore(o => o.Type);

                p.Ignore(o => o.ValueType);

                p.Property(o=>o.Id)
                    .UseHiLo("dec_record_attribute_seq");
               
                p.Property(o => o.DescriptionType)
                    .HasConversion<StringToTypeConverter>();
            });
       
        builder.OwnsMany(o => o.StringAttributes,
            p =>
            {
                p.Ignore(o => o.DomainEvents);
              
                p.Ignore(o => o.Type);

                p.Ignore(o => o.ValueType);

                p.Property(o=>o.Id)
                    .UseHiLo("str_record_attribute_seq");
               
                p.Property(o => o.DescriptionType)
                    .HasConversion<StringToTypeConverter>();
            });
        
        builder.OwnsMany(o => o.CatalogRecordItemAttributes,
            p =>
            {
                p.Ignore(o => o.DomainEvents);
              
                p.Ignore(o => o.Type);

                p.Ignore(o => o.ValueType);

                p.Property(o=>o.Id)
                    .UseHiLo("catrec_record_attribute_seq");
               
                p.Property(o => o.DescriptionType)
                    .HasConversion<StringToTypeConverter>();
            });

    }
}