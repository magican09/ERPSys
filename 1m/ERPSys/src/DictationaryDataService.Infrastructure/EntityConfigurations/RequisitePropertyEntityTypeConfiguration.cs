using DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate.RequisiteProperties;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictationaryDataService.Infrastructure.EntityConfigurations;

public class RequisitePropertyEntityTypeConfiguration:IEntityTypeConfiguration<RequisitePropertyItem>
{
    public void Configure(EntityTypeBuilder<RequisitePropertyItem> builder)
    {
        builder.ToTable("RequisiteProperties");
    
        builder.Ignore(o => o.DomainEvents);
        
        builder.Property(o => o.Id)
            .UseHiLo("requisite_properties_seq");
        
        builder.Property(o=>o.ReqisiteType)
            .HasConversion(
                v=>v.ToString(),
                v=>(Type)Enum.Parse(typeof(Type), v));

        builder.Property(o=>o.RequisiteValueType)
            .HasConversion(
                v=>v.ToString(),
                v=>(Type)Enum.Parse(typeof(Type), v));

        
       // builder.Property<int>("dictationaryDataId");
    }
}