using MediatR;

namespace ConsoleAppMediatpRTest;

public class CreateToDoItem:IRequest<int>
{
    public string ToDoItemText { get; set; }
}