using MediatR;

namespace ConsoleAppMediatpRTest;

public class CreateToDoItemHandler:IRequestHandler<CreateToDoItem,int>
{
    public Task<int> Handle(CreateToDoItem request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}