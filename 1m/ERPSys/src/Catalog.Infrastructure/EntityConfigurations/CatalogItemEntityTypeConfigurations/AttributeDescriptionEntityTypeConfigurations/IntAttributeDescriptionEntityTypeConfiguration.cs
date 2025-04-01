using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class IntAttributeDescriptionEntityTypeConfiguration:IEntityTypeConfiguration<IntAttributeDescription>
{
    public void Configure(EntityTypeBuilder<IntAttributeDescription> builder)
    {
   //    builder.ToTable("IntAttributeDescriptions");
     //  builder.Ignore(o => o.DomainEvents);
     //  builder.Property(o => o.Id)
       //    .UseHiLo("int_attribute_description_seq");
       /*builder.Property(o=>o.AttributeType)
           .HasConversion(
               v=>v.ToString(),
               v=>(Type)Enum.Parse(typeof(Type), v));*/
      
    }
}