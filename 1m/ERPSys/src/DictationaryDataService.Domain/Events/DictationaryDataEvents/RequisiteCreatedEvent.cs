namespace DictationaryDataService.Domain.Events;

public class RequisiteCreatedEvent:INotification
{
    public int DictationaryDataId { get;  }
    public RequisitePropertyItem RequisitePropertyItem{ get;  }
    public RequisiteCreatedEvent(RequisitePropertyItem requisitePropertyItem, int dictationaryDataId)
    {
        DictationaryDataId = dictationaryDataId;
        RequisitePropertyItem = requisitePropertyItem;
    }
}