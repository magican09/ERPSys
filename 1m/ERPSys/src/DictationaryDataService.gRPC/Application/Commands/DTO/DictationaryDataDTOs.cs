namespace DictationaryDataService.gRPC.Application.Commands.DTO;

public record RequisiteDTO
{
    public string Name { get; init; }
    public string Value { get; init; }
}

public record DictationaryDataItemDTO()
{
   public IEnumerable<RequisiteDTO> IntRequisites { get; init; }
   public IEnumerable<RequisiteDTO> StringRequisites { get; init; }
   public IEnumerable<RequisiteDTO> DecimalRequisites { get; init; }
   public IEnumerable<RequisiteDTO> DictationaryDataRequisites { get; init; }
   
}

public record RequisitePropertyItemDTO
{
    public string RequisiteName { get; init; }
    public string RequisiteValueType { get; init; }
    public string RequisiteType { get; init; }
}

public record DictationaryDataDTO
{
    public string Name { get; init; }
    public IEnumerable<RequisitePropertyItemDTO> RequisiteProperties { get; init; }
}