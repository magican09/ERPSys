using Catalog.gRPC.Application.Models;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddNewCatalogRecordItemCommand:IRequest<int>
{
    public CatalogRecordItemDTO CatalogRecordItemDTO
    {
        get; init; 
        
    }

    public AddNewCatalogRecordItemCommand(CatalogRecordItemAppModel catalogRecordItemAppModel)
    {
        CatalogRecordItemDTO = new CatalogRecordItemDTO
        {
            CatalogitemId = catalogRecordItemAppModel.CatalogItemId,
        };

    }
    
}

public class CatalogRecordItemDTO
{
    public string id { get; init; }
    
    public string CatalogitemId { get; init; }
    
    public IEnumerable<AttributeDTO> Attributes { get; init; }
}

public class AttributeDTO {
   public string Id{ get; init; }
   public string Name { get; init; }
   public string Type { get; init; }
   public string Value { get; init; }
}