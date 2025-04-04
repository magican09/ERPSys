using Catalog.gRPC.Application.Commands;
using FluentValidation;

namespace Catalog.gRPC.Application.Validators;

public class IdentifiedCommandValidator:AbstractValidator<IdentifiedCommand<CreateCatalogCommand,int>>
{
    public IdentifiedCommandValidator(ILogger<IdentifiedCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
    
}