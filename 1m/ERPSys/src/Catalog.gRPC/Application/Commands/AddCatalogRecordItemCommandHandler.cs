using System.Text.Json;
using Catalog.gRPC.Infrastructure.Services;
using Catalog.Infrastructure.Idempotency;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;
using Catalogs.Domain.Events;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddCatalogRecordItemCommandHandler:IRequestHandler<AddCatalogRecordItemCommand,int>
{
    
    private readonly ICatalogRepository _catalogRepository;
    private readonly ICatalogRecordItemRepository _catalogRecordItemRepository;
    private readonly IIdentityService _identityService;
    private readonly ILogger<AddCatalogRecordItemCommandHandler> _logger;

    public AddCatalogRecordItemCommandHandler(ICatalogRepository catalogRepository, ICatalogRecordItemRepository catalogRecordItemRepository, IIdentityService identityService,
        ILogger<AddCatalogRecordItemCommandHandler> logger)
    {
        _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        _catalogRecordItemRepository = catalogRecordItemRepository ?? throw new ArgumentNullException(nameof(catalogRecordItemRepository));
        _catalogRecordItemRepository = catalogRecordItemRepository ?? throw new ArgumentNullException(nameof(catalogRecordItemRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));     
    }
 
    public async Task<int> Handle(AddCatalogRecordItemCommand message, CancellationToken cancellationToken)
    {
        var addedCatalorRecordItemDTO = message.CatalogRecordItemDTO;
        
        var catalor = await _catalogRepository.GetAsync( Int32.Parse(addedCatalorRecordItemDTO.CatalogitemId));

        var newCatalogRecorditem = new CatalogRecordItem(catalor);

        foreach (var attributeDto in addedCatalorRecordItemDTO.Attributes)
        {
             newCatalogRecorditem.UpdateAttribute(attributeDto.Name,attributeDto.Type, attributeDto.Value);
        }
        
        _catalogRecordItemRepository.Add(newCatalogRecorditem);
        
        newCatalogRecorditem.AddDomainEvent(new CatalogRecordItemAddedEvent(newCatalogRecorditem.Id));

        _catalogRecordItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return newCatalogRecorditem.Id;
    }
}


public class AddCatalogRecordItemIdentityCimmandHandler : IdentifiedCommandHandler<AddCatalogRecordItemCommand, int>
{
    public AddCatalogRecordItemIdentityCimmandHandler(IMediator mediator, IRequestManager requestManager, ILogger<IdentifiedCommandHandler<AddCatalogRecordItemCommand, int>> logger) : base(mediator, requestManager, logger)
    {
    }

    protected override int CreateResultForDuplicateRequest()
    {
        return default;

    }
}