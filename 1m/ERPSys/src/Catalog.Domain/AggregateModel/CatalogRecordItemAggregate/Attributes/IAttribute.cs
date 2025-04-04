
namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;


public interface IAttribute
{
    Type Type { get; }
    Type ValueType { get; }
    string Name { get; }
    Type DescriptionType { get; }
    
    
}