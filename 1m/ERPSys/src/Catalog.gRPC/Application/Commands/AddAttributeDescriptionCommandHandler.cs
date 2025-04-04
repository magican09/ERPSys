using Catalog.Infrastructure.Idempotency;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddAttributeDescriptionCommandHandler:AddAttributeDescriptionIdentityCommandHandler<AddAttributeDescriptionCommand,(int,int)>
{
    
}

public class AddAttributeDescriptionIdentityCommandHandler : IdentifiedCommandHandler<AddAttributeDescriptionCommand,(int,int)>
{
    public AddAttributeDescriptionIdentityCommandHandler(IMediator mediator, IRequestManager requestManager, 
        ILogger<IdentifiedCommandHandler<AddAttributeDescriptionCommand, (int, int)>> logger) : base(mediator, requestManager, logger)
    {
        
    }

    protected override (int, int) CreateResultForDuplicateRequest()
    {
        return (0, 0);
    }
}