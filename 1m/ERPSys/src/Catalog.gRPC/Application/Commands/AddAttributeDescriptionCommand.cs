using Catalog.gRPC.Application.Models;
using MediatR;

namespace Catalog.gRPC.Application.Commands;

public class AddAttributeDescriptionCommand:IRequest<(int,int)>
{
    public AttributeDescriptionDTO AttributeDescription { get; init; }

    public AddAttributeDescriptionCommand(AttributeDescriptionAppModel attributeDescription)
    {
        AttributeDescription = new   AttributeDescriptionDTO
            {
                CatalogItemId = attributeDescription.CatalogItemId,
                AttributeName = attributeDescription.AttributeName,
                Description = attributeDescription.Description,
                Synonym = attributeDescription.Synonym,
                AttributeTypeName = attributeDescription.AttributeTypeName,
                Properties = attributeDescription.Properties
                    .Select(p=> 
                        new KeyValuePair<string,string>(p.Key,p.Value))
                    .ToDictionary(k=>k.Key,v=>v.Value)
            };
            
           
        
    }
    
}
