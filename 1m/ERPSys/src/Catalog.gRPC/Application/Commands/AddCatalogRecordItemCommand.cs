using Catalog.gRPC.Application.Models;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddCatalogRecordItemCommand:IRequest<int>
{
    public CatalogRecordItemDTO CatalogRecordItemDTO { get;  }

    public AddCatalogRecordItemCommand(CatalogRecordItemAppModel   catalogRecordItemAppModel)
    {
        CatalogRecordItemDTO = new CatalogRecordItemDTO
        {
            CatalogitemId = catalogRecordItemAppModel.CatalogItemId,
            Attributes = catalogRecordItemAppModel.Attributes.Select(
                a => new AttributeDTO
                {
                    Name = a.Name,
                    Value = a.Value,
                    Type = a.Type
                })
        };
    }
}