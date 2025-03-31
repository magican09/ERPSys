namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate.Requisites;

public class StringRequisite:Requisite<string>
{
    public StringRequisite(string name, string? value) : base(name, value)
    {
    }
}