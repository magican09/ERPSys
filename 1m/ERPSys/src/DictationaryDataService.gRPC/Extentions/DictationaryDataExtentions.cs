using DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate;

namespace DictationaryDataService.gRPC.Extentions;

public static class DictationaryDataExtentions
{
    public static IEnumerable<RequisitePropertyItemDTO> ToRequisitePropertyItemsDTO(
        this IEnumerable<RequisitePropertyItem> requisitePropertyItems)
    {
        foreach (var item in requisitePropertyItems)
        {
            yield return item.ToRequisitePropertyItemDTO();
        }
    }

    public static RequisitePropertyItemDTO ToRequisitePropertyItemDTO(this RequisitePropertyItem requisitePropertyItem)
    {
        return new RequisitePropertyItemDTO()
        {
            RequisiteName = requisitePropertyItem.RequisiteName,
            RequisiteValueType = requisitePropertyItem.RequisiteValueType.ToString(),
            RequisiteType = requisitePropertyItem.ReqisiteType.ToString()
        };
    }
}