using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class AttributeDescriptionEntityTypeConfiguration:IEntityTypeConfiguration<AttributeDescription>
{
    public void Configure(EntityTypeBuilder<AttributeDescription> builder)
    {
      // builder.ToTable("AttributeDescriptions");
       builder.Ignore(o => o.DomainEvents);
       /*builder.Property(o => o.Id)
           .UseHiLo("attribute_description_seq");*/
       builder.Property(o=>o.AttributeType)
           .HasConversion(
               v=>v.ToString(),
               v=>(Type)Enum.Parse(typeof(Type), v));
    }
}