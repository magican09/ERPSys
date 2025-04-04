using Catalog.gRPC.Infrastructure.Services;
using Catalog.Infrastructure.Idempotency;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;
using Catalogs.Domain.Events;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class CreateCatalogCommandHandler:IRequestHandler<CreateCatalogCommand,int>
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly ILogger<CreateCatalogCommandHandler> _logger;

    public CreateCatalogCommandHandler (IMediator mediator,
//        IOrderingIntegrationEventService orderingIntegrationEventService,
        ICatalogRepository catalogRepository,
        IIdentityService identityService,
     ILogger<CreateCatalogCommandHandler> logger)
    {
        _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
       //  _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<int> Handle(CreateCatalogCommand message, CancellationToken cancellationToken)
    {
      
       
        var existingCatalog = await _catalogRepository.GetByNameAsync(message.CatalogItemDTO.Name);

        if (existingCatalog != null)
        {
            _logger.LogWarning($"Catalog with name : {message.CatalogItemDTO.Name} already exists");      
            throw new ApplicationException($"Catalog with name : {message.CatalogItemDTO.Name} already exists");
        }
        
        _logger.LogInformation($"Creating catalog with name: {message.CatalogItemDTO.Name}");
        var catalogItemDTO = message.CatalogItemDTO;
        
        var catalog = new CatalogItem(
            catalogItemDTO.Name,
            catalogItemDTO.Synonym,
            catalogItemDTO.Code,
            catalogItemDTO.CodeType,
            catalogItemDTO.CodeLength,
            catalogItemDTO.CodeAllowedLength,
            catalogItemDTO.Description,
            catalogItemDTO.DescriptionLength,
            catalogItemDTO.CreateOnInput,
            catalogItemDTO.DataLockControlMode,
            catalogItemDTO.FullTextSearch,
            catalogItemDTO.LevelCount,
            catalogItemDTO.FoldersOnTop,
            catalogItemDTO.CheckUnique,
            catalogItemDTO.Autonumbering,
            catalogItemDTO.DefaultPresentation,
            catalogItemDTO.EditType,
            catalogItemDTO.ChoiceMode,
            catalogItemDTO.UseStandardCommands
            );
        foreach (var dtoAtrDescription in message.CatalogItemDTO.AttributeDescriptions)
        {
            var attrDescription = AttributeDescripionFactory.GetAttributeDescription(dtoAtrDescription.AttributeTypeName);
            attrDescription.AttributeName = dtoAtrDescription.AttributeName;
            attrDescription.Description = dtoAtrDescription.Description;
            attrDescription.Synonym=   dtoAtrDescription.Synonym;
            AttributeDescriptionHelper.SetAttributeDescriptionProperties(attrDescription,dtoAtrDescription.Properties);
            catalog.AddAttribute(attrDescription);
        }
        
        _catalogRepository.Add(catalog);
       
        catalog.AddDomainEvent(new CatalogItemCreatedEvent(catalog));
        
        await _catalogRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return  catalog.Id;
    }
}

// Use for Idempotency in Command process
public class CreateCatalogIdentityCommandHandler : IdentifiedCommandHandler<CreateCatalogCommand, int>
{
    private IdentifiedCommandHandler<CreateCatalogCommand, int> _identifiedCommandHandlerImplementation;

    public CreateCatalogIdentityCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CreateCatalogCommand, int>> logger)
        : base(mediator, requestManager, logger)
    {
        
    }

    protected override int CreateResultForDuplicateRequest()
    {
        return default;
    }
}
