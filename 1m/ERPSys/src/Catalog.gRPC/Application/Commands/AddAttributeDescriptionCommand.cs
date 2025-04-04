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
                AttributeName = attributeDescription.AttributeName,
                Description = attributeDescription.Description,
                Synonym = attributeDescription.Synonym,
                AttributeTypeName = attributeDescription.AttributeTypeName,
                Properties = AttributeDescription.Properties
                    .Select(p=> 
                        new KeyValuePair<string,string>(p.Key,p.Value))
                    .ToDictionary(k=>k.Key,v=>v.Value)
            };
            
           
        
    }
    
}
