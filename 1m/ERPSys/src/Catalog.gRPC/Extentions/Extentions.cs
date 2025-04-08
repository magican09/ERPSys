using Catalog.gRPC.Apis;
using Catalog.gRPC.Application.Commands;
using Catalog.gRPC.Application.Queies;
using Catalog.gRPC.Application.Validators;
using Catalog.gRPC.Infrastructure.Services;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Idempotency;
using Catalog.Infrastructure.Repositories;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.gRPC.Extentions;

internal static class Extensions
{
    public static void AddApplictionaServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
      
        // Add the authentication services to DI
      //  builder.AddDefaultAuthentication();

        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        services.AddDbContext<CatalogsContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB"));
        });
        builder.EnrichNpgsqlDbContext<CatalogsContext>();

       // services.AddMigration<OrderingContext, OrderingContextSeed>();

        // Add the integration services that consume the DbContext
       // services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrderingContext>>();

      //  services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

       // builder.AddRabbitMqEventBus("eventbus")
        //    .AddEventBusSubscriptions();

        services.AddHttpContextAccessor();
        services.AddTransient<IIdentityService, IdentityService>();

        // Configure mediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

          //  cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
         //   cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
          //  cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
         
          services.AddScoped<ICatalogServices, CatalogServices>();
            
          services.AddScoped<ICatalogItemQueries, CatalogItemQueries>();
         
          services.AddScoped<ICatalogRepository, CatalogItemRepository>();
          services.AddScoped<ICatalogRecordItemRepository, CatalogRecordItemRepository>();
          services.AddScoped<IRequestManager, RequestManager>();

    }
}