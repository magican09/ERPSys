namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate.Requisites;

public class DecimalRequisite:Requisite<decimal>
{
    public DecimalRequisite(string name, decimal value) : base(name, value)
    {
    }
}