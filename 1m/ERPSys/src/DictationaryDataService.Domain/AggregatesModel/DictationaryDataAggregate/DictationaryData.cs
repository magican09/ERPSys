

using DictationaryDataService.Domain.Exceptions;

namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate;

public class DictationaryData:Entity,IAggregateRoot
{
    public string Name { get; private set; }
    private List<RequisitePropertyItem> _requisiteProperties;

    public IReadOnlyCollection<RequisitePropertyItem> RequisiteProperties => _requisiteProperties.AsReadOnly();

    public DictationaryData()
    {
        _requisiteProperties = new List<RequisitePropertyItem>();
    }

    public void AddRequisiteProperty(string requisiteName,Type requisiteType)
    {
        var existedRequisiteproperty = _requisiteProperties
            .FirstOrDefault(rp=>rp.RequisiteName == requisiteName);
        if(existedRequisiteproperty != null)
            throw new DictationaryDataExceptions($"Duplicate requisite name: {requisiteName}");
        
        var newRequisiteProperty = new RequisitePropertyItem()
        {
            RequisiteName = requisiteName,
            ReqisiteType = requisiteType,
            RequisiteValueType = requisiteType.GetGenericArguments()[0]
        };
        
        _requisiteProperties.Add(newRequisiteProperty);
        this.AddDomainEvent(new RequisiteCreatedEvent(newRequisiteProperty,this.Id));
    }

    public void ChangeName(string newName)
    {
        Name = newName;
        this.AddDomainEvent( new DictationaryDataNameChangedEvent(newName,this.Id));
    }
}