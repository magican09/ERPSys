using Catalog.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class ClientRequestEntityTypeConfiguration: IEntityTypeConfiguration<ClientRequest>
{
    public void Configure(EntityTypeBuilder<ClientRequest> builder)
    {
       builder.ToTable("requests");
    }
}