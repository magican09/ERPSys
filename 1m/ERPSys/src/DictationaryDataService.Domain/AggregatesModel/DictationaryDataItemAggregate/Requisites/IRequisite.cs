namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate.Requisites;

public interface IRequisite
{
    string Name { get; }
    Type ValueType { get; }
    Type Type { get; }
    }