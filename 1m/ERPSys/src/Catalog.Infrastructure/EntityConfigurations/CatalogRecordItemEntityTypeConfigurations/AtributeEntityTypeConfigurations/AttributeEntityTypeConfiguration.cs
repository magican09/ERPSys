using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes.Attribute;

namespace Catalog.Infrastructure.EntityConfigurations.AtributeEntityTypeConfigurations;

public class AttributeEntityTypeConfiguration: IEntityTypeConfiguration<Attribute>
{
    public void Configure(EntityTypeBuilder<Attribute> builder)
    {
       builder.ToTable("Attributes");
       builder.Ignore(o => o.Type);
       builder.Ignore(o => o.ValueType);
       builder.Property(o => o.Value)
           .HasConversion(
               v=>JsonSerializer.Serialize(v,(JsonSerializerOptions)null),
               v=>JsonSerializer.Deserialize<object>(v,(JsonSerializerOptions)null));
    }
}