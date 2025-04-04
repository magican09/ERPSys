using Catalog.gRPC.Application.Queies;
using Catalog.gRPC.Infrastructure.Services;
using MediatR;

namespace Catalog.gRPC.Apis;

public interface ICatalogServices
{
    IMediator Mediator { get; set; }
    ICatalogItemQueries CatalogItemQueries { get; set; }
    IIdentityService IdentityService { get; set; }
    ILogger<CatalogServices> Logger { get; set; }
}

public class CatalogServices(
    IMediator mediator,
    ICatalogItemQueries catalogItemQueries,
    IIdentityService identityService,
    ILogger<CatalogServices> logger
    ) : ICatalogServices
{
    public IMediator Mediator { get; set; }=mediator;
    public ICatalogItemQueries CatalogItemQueries { get; set; }=catalogItemQueries;
    public IIdentityService IdentityService { get; set; }=identityService;
    public ILogger<CatalogServices> Logger { get; set; }=logger;
    
}