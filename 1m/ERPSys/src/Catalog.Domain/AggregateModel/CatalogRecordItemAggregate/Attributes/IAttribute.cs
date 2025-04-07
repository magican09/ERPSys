
namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;


public interface IAttribute:IEntity
{
    Type Type { get; }
    Type ValueType { get; }
    string Name { get; }
    Type DescriptionType { get; }
    
    public void SetValue(object value);
    
    
}