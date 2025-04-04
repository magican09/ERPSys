using Catalog.Infrastructure.Idempotency;
using EventBus.Extentions;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public abstract class IdentifiedCommandHandler<T,R> : IRequestHandler<IdentifiedCommand<T, R>, R> 
    where T : IRequest<R>
{ 
    
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;
    private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;

    public IdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<T, R>> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _mediator = mediator;
        _requestManager = requestManager;
        _logger = logger;
    }
    /// <summary>
    /// Creates the result value to return if a previous request was found
    /// </summary>
    /// <returns></returns>
    protected abstract R CreateResultForDuplicateRequest();
    
    public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
    {
        var alreadyExists = await _requestManager.ExistAsync(message.Id);

        if (alreadyExists)
        {
            return CreateResultForDuplicateRequest();
        }
        else
        {
            await _requestManager.CreateRequestForCommandAsync<T>(message.Id);

            try
            {
                var command = message.Command;
                var commandName = command.GetGenericTypeName();
                var idProperty = string.Empty;
                var commandId= string.Empty;
                switch (command)
                {
                    case CreateCatalogCommand createCatalogCommand:
                      //  idProperty=nameof(createCatalogCommand.)
                        break;
                    default:
                        idProperty = "id?";
                        commandId = "n/a";
                        break;
                }
                
                _logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    commandName,
                    idProperty,
                    commandId,
                    command);

                // Send the embedded business command to mediator so it runs its related CommandHandler 
                var result = await _mediator.Send(command, cancellationToken);

                _logger.LogInformation(
                    "Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    result,
                    commandName,
                    idProperty,
                    commandId,
                    command);
                
                return result;
            }
            catch (Exception e)
            {
                return default(R);
            }
            
        }

        return CreateResultForDuplicateRequest();   //Для тестирования 
        
    }
}