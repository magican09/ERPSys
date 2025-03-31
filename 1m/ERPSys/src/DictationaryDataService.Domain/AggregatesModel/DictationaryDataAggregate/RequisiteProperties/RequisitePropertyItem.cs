namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate.RequisiteProperties;

public class RequisitePropertyItem:Entity
{
    public string RequisiteName { get; internal set; }
    public Type RequisiteValueType { get; internal set; }
    public Type ReqisiteType { get; internal set; }
    
}