using Catalog.gRPC.Infrastructure.Services;
using Catalog.gRPC.v1;
using Catalog.Infrastructure.Idempotency;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Catalogs.Domain.Events.AttributeDescriptionEvents;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddAttributeDescriptionCommandHandler:IRequestHandler<AddAttributeDescriptionCommand,(int,int)>
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly ILogger<AddAttributeDescriptionCommandHandler> _logger;
    public AddAttributeDescriptionCommandHandler( IMediator mediator,
        ICatalogRepository catalogRepository,
        IIdentityService identityService, ILogger<AddAttributeDescriptionCommandHandler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<(int, int)> Handle(AddAttributeDescriptionCommand message, CancellationToken cancellationToken)
    {
        var attributeDesctiptionDTO = message.AttributeDescription;
        
        var catalogItem = await _catalogRepository.GetAsync(int.Parse(attributeDesctiptionDTO.CatalogItemId));

        _logger.LogInformation($"Add attribute description to catalog id={attributeDesctiptionDTO.CatalogItemId}");

        var newAtribiteDescription = AttributeDescripionFactory.CreateAttributeDescription(attributeDesctiptionDTO.AttributeTypeName);
        
        newAtribiteDescription.AttributeName = attributeDesctiptionDTO.AttributeName;
        newAtribiteDescription.Description = attributeDesctiptionDTO.Description;
        newAtribiteDescription.Synonym = attributeDesctiptionDTO.Synonym;
        CatalogItem.SetAttributeDescriptionProperties(newAtribiteDescription,
            attributeDesctiptionDTO.Properties
                .Select(p_kvp =>
                    new KeyValuePair<string, object>(p_kvp.Key, p_kvp.Value))
                .ToDictionary()
                );     
        
        //AttributeDescriptionHelper.SetAttributeDescriptionProperties(newAtribiteDescription,attributeDesctiptionDTO.Properties);

        try
        {
            catalogItem.AddAttributeDescription(newAtribiteDescription);

        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error while adding attribute description {newAtribiteDescription.AttributeName}");
        }
     
        await _catalogRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken); 
        
        return (catalogItem.Id, newAtribiteDescription.Id);
    }
}

public class AddAttributeDescriptionIdentityCommandHandler : IdentifiedCommandHandler<AddAttributeDescriptionCommand,(int,int)>
{
    public AddAttributeDescriptionIdentityCommandHandler(IMediator mediator, IRequestManager requestManager, 
        ILogger<IdentifiedCommandHandler<AddAttributeDescriptionCommand, (int, int)>> logger) : base(mediator, requestManager, logger)
    {
        
    }

    protected override (int, int) CreateResultForDuplicateRequest()
    {
        return (default(int), default(int));
    }
}