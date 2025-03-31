using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictationaryDataService.Infrastructure.EntityConfigurations;

public class DictationaryDataEntityTypeConfiguration:IEntityTypeConfiguration<DictationaryData>
{
    public void Configure(EntityTypeBuilder<DictationaryData> builder)
    {
       builder.ToTable("DictationaryData");
       builder.Ignore(o => o.DomainEvents);
       
       builder.Property(p => p.Id)
           .UseHiLo("dictationaryData_seq");

       builder.OwnsMany(o => o.RequisiteProperties,
           rd =>
           {
               rd.Property(p => p.Id)
                   .UseHiLo("requisitePropperty_seq");
               
               rd.Ignore(p => p.DomainEvents);
               
               rd.Property(p => p.ReqisiteType)
                   .HasConversion(
                       v => v.ToString(),
                       v => (Type)Enum.Parse(typeof(Type), v));
            
               rd.Property(p => p.RequisiteValueType)
                   .HasConversion(
                       v => v.ToString(),
                       v => (Type)Enum.Parse(typeof(Type), v));
           });

    }
}