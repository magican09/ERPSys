using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration:IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");
        
        builder.Ignore(o=>o.DomainEvents);

  //      builder.HasKey(c =>new {c.Id ,c.Name});
           

        builder.Property(o => o.Id)
            .UseHiLo("catalog_seq");
        builder.OwnsMany(p => p.IntAttributeDescriptions,
            o =>
            {
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
               /* o.Ignore(o => o.AttributeType);
                o.Ignore(o => o.Description);
                o.Ignore(o => o.Synonym);
                o.Ignore(o => o.AttributeName);*/
               
                o.Property(o=>o.Id)
                    .UseHiLo("int_attribute_description_seq");
                
             
                o.Property(o=>o.AttributeType)
                    .HasConversion(
                        v=>v.ToString(),
                        v=>(Type)Enum.Parse(typeof(Type), v));
                
            });
        /*builder.OwnsMany(p => p.DecimalAttributeDescriptions,
            o =>
            {
               
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
                
                o.Property(o=>o.Id)
                    .UseHiLo("int_attribute_description_seq");
                
                o.Property(o=>o.AttributeValueType)
                    .HasConversion(
                        v=>v.ToString(),
                        v=>(Type)Enum.Parse(typeof(Type), v));
                
            });
        builder.OwnsMany(p => p.StringAttributeDescriptions,
            o =>
            {
               
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
                
                o.Property(o=>o.Id)
                    .UseHiLo("int_attribute_description_seq");
                
                o.Property(o=>o.AttributeValueType)
                    .HasConversion(
                        v=>v.ToString(),
                        v=>(Type)Enum.Parse(typeof(Type), v));
                
            });
        builder.OwnsMany(p => p.CatalogRecordItemAttributeDescriptions,
            o =>
            {
               
                o.Ignore(o => o.DomainEvents);
                o.Ignore(o => o.Type);
                
                o.Property(o=>o.Id)
                    .UseHiLo("int_attribute_description_seq");
                
                o.Property(o=>o.AttributeValueType)
                    .HasConversion(
                        v=>v.ToString(),
                        v=>(Type)Enum.Parse(typeof(Type), v));
                
            });*/
       
    }
}