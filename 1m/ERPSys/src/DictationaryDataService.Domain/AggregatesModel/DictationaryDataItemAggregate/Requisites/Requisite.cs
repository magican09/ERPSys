using DictationaryDataService.Domain.SeedWork;

namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate.Requisites;

public class Requisite<T>:Entity, IRequisite
{
    public string Name { get; private set; }
    public Type ValueType  => typeof(T);
    public Type Type => this.GetType(); 
    public T? Value { get; set; }
    /*protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return ValueType;
        yield return Type;
        yield return Value;
    }*/

    public Requisite()
    {
        
    }
    public Requisite(string name, T? value)
    {
        Name = name;
        Value = value;
    }
}