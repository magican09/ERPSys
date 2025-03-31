namespace DictationaryDataService.Domain.Exceptions;

public class DictationaryDataExceptions:Exception
{
    public DictationaryDataExceptions() { }
    
    public DictationaryDataExceptions(string message) : base(message) { }
    public DictationaryDataExceptions(string message, Exception inner) : base(message, inner) { }
    
}