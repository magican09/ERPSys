namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate.Requisites;

public class IntRequisite: Requisite<int>
{
    public IntRequisite(string name, int value):base(name, value)
    {
        
    }
}