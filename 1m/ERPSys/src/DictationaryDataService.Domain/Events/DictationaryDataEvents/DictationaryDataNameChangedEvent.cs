namespace DictationaryDataService.Domain.Events;

public class DictationaryDataNameChangedEvent:INotification
{
    public int DictationaryDataId { get;  }
    public   string NewName { get;  }

    public DictationaryDataNameChangedEvent(string newName, int dictationaryDataId)
    {
        DictationaryDataId = dictationaryDataId;
        NewName = newName;
    }
}