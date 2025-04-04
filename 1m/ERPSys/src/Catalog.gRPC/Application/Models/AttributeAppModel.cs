namespace Catalog.gRPC.Application.Models;

public class AttributeAppModel
{
    public string Name { get; init; }
    public object Value  { get; init; }
    public Type ValueType  { get; init; }
    public Type Type  { get; init; }
    
}