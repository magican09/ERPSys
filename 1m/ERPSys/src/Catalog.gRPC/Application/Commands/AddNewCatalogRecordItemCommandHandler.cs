using Catalog.gRPC.Infrastructure.Services;
using Catalog.Infrastructure.Idempotency;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using Catalogs.Domain.Events;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddNewCatalogRecordItemCommandHandler:IRequestHandler<AddNewCatalogRecordItemCommand,int>
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly ICatalogRecordItemRepository _catalogRecordItemRepository;
    private readonly IIdentityService _identityService;
    private readonly ILogger<AddNewCatalogRecordItemCommandHandler> _logger;
    private readonly IMediator _mediator;

    public AddNewCatalogRecordItemCommandHandler(IMediator mediator,
        ICatalogRepository catalogRepository,
        ICatalogRecordItemRepository catalogRecordItemRepository,
        IIdentityService identityService,
        ILogger<AddNewCatalogRecordItemCommandHandler> logger)
    {
        _mediator = mediator??throw new ArgumentNullException(nameof(mediator));
        _catalogRepository = catalogRepository??throw new ArgumentNullException(nameof(catalogRepository));
        _catalogRecordItemRepository = catalogRecordItemRepository??throw new ArgumentNullException(nameof(catalogRecordItemRepository));
        _identityService = identityService??throw new ArgumentNullException(nameof(identityService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
    }
    
    public async Task<int> Handle(AddNewCatalogRecordItemCommand message, CancellationToken cancellationToken)
    {
        var catalogRecordItemDTO = message.CatalogRecordItemDTO;

        var  catalogItem = await _catalogRepository.GetAsync(Int32.Parse(catalogRecordItemDTO.CatalogitemId));
        
        var newCatalogRecordItem = new CatalogRecordItem(catalogItem);
        
        var addedCatalogRecordItem =  _catalogRecordItemRepository.Add(newCatalogRecordItem);
        
        addedCatalogRecordItem.AddDomainEvent(new CatalogRecordItemAddedEvent(addedCatalogRecordItem.Id));
        
        await _catalogRecordItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
      
        return addedCatalogRecordItem.Id;
    }
}


public class AddNewCatalogRecordItemIdenticalCommandHandler : IdentifiedCommandHandler<AddNewCatalogRecordItemCommand, int>
{
    public AddNewCatalogRecordItemIdenticalCommandHandler(IMediator mediator, IRequestManager requestManager, ILogger<IdentifiedCommandHandler<AddNewCatalogRecordItemCommand, int>> logger) : base(mediator, requestManager, logger)
    {
    }

    protected override int CreateResultForDuplicateRequest()
    {
        return default(int);
    }
}