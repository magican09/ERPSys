using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictationaryDataService.Infrastructure.EntityConfigurations;

public class DictationaryDataItemEntityTypeConfiguration:IEntityTypeConfiguration<DictationaryDataItem>
{
    public void Configure(EntityTypeBuilder<DictationaryDataItem> builder)
    {
         builder.ToTable("DictationaryDataItem");
        
         builder.Property(p => p.Id)
             .UseHiLo("dictationaryDataItem_seq");
         
         builder.Ignore(o => o.DomainEvents);

         builder.OwnsMany(o => o.IntRequisites,
             r =>
             {    
                 r.Ignore(p => p.DomainEvents);
                 r.Ignore(p => p.Type);
                 r.Ignore(p => p.ValueType);
             });
         builder.OwnsMany(o => o.DecimalRequisites,
             r =>
             {    
                 r.Ignore(p => p.DomainEvents);
                 r.Ignore(p => p.Type);
                 r.Ignore(p => p.ValueType);
             });
         builder.OwnsMany(o => o.StringRequisites,
             r =>
             {    
                 r.Ignore(p => p.DomainEvents);
                 r.Ignore(p => p.Type);
                 r.Ignore(p => p.ValueType);
             });
         builder.OwnsMany(o => o.DictationaryDataItemsRequisites,
             r =>
             {    
                 r.Ignore(p => p.DomainEvents);
                 r.Ignore(p => p.Type);
                 r.Ignore(p => p.ValueType);
             });

         /*builder.OwnsMany(o => o.IntRequisites,
             r =>
                     {
                         r.WithOwner().HasForeignKey("dictationaryDataItem_id");
                         r.Property<int>("id");
                         r.HasKey("id");
                         r.Ignore(p => p.Type)
                             .Ignore(p => p.ValueType);
                     }
                 );

                 builder.OwnsMany(o => o.DecimalRequisites,
                     r =>
                     {
                         r.WithOwner().HasForeignKey("dictationaryDataItem_id");
                         r.Property<int>("id");
                         r.HasKey("id");
                         r.Ignore(p => p.Type)
                             .Ignore(p => p.ValueType);
                     }
                 );
                 builder.OwnsMany(o => o.StringRequisites,
                     r =>
                     {
                         r.WithOwner().HasForeignKey("dictationaryDataItem_id");
                         r.Property<int>("id");
                         r.HasKey("id");
                         r.Ignore(p => p.Type)
                             .Ignore(p => p.ValueType);
                     }
                 );
                 builder.OwnsMany(o => o.DictationaryDataItemsRequisites,
                     r =>
                     {
                         r.WithOwner().HasForeignKey("dictationaryDataItem_id");
                         r.Property<int>("id");
                         r.HasKey("id");
                         r.Ignore(p => p.Type)
                             .Ignore(p => p.ValueType);
                     }
                 );*/
    }
}