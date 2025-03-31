using System.Security.Principal;
using DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate;
using DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate;
using DictationaryDataService.gRPC.Infrastructure.Services;

namespace DictationaryDataService.gRPC.Application.Commands;

public class CreateDictationaryDataCommandHandler:IRequestHandler<CreateDictationaryDataCommand,bool>
{
    private readonly IDictationaryDataRepository _dictationaryDataRepository;
   // private readonly IDictationaryDataItemRepository _dictationaryDataitemRepository;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
   // private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
    private readonly ILogger<CreateDictationaryDataCommandHandler> _logger;

    public CreateDictationaryDataCommandHandler(IMediator mediator,
        IDictationaryDataRepository dictationaryDataRepository,
        IDictationaryDataItemRepository dictationaryDataitemRepository,
        IIdentityService identityService,
        ILogger<CreateDictationaryDataCommandHandler> logger
        )
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _dictationaryDataRepository = dictationaryDataRepository;
      //  _dictationaryDataitemRepository = dictationaryDataitemRepository;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _identityService= identityService;
        
    }
    public async Task<bool> Handle(CreateDictationaryDataCommand message, CancellationToken cancellationToken)
    {
        var dictationaryData = new DictationaryData();
        dictationaryData.ChangeName(message.Name);
        foreach (var item in message.RequisiteProperties)
        {
            var requisiteType = (Type) Enum.Parse(typeof(Type), item.RequisiteType);
            dictationaryData.AddRequisiteProperty(item.RequisiteName,requisiteType);
        }
        
        _logger.LogInformation("Creating DictationaryData: {@DictationaryData}", dictationaryData);
        
       return await _dictationaryDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}