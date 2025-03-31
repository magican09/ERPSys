



using DictationaryDataService.gRPC.Extentions;

namespace DictationaryDataService.gRPC.Application.Commands;

[DataContract]
public class CreateDictationaryDataCommand:IRequest<bool>
{
    [DataMember] 
    private readonly List<RequisitePropertyItemDTO> _requisiteProperties;
   
    [DataMember]
    public string Name { get; private set; }
    
    [DataMember]
    public string UserId { get; private set; }
    
    [DataMember]
    public string UserName { get; private set; }

    [DataMember] public IEnumerable<RequisitePropertyItemDTO> RequisiteProperties => _requisiteProperties;

    public CreateDictationaryDataCommand()
    {
            _requisiteProperties = new List<RequisitePropertyItemDTO>();
    }

    public CreateDictationaryDataCommand( string userName, string userId,string name, List<RequisitePropertyItem> requisiteProperties )
    {
            _requisiteProperties = requisiteProperties.ToRequisitePropertyItemsDTO().ToList();
            UserId = userId;
            UserName = userName;
    }
}