namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate;

public interface IDictationaryDataItemRepository
{
    DictationaryDataItem Add(DictationaryDataItem dictationaryDataItem);
    DictationaryDataItem Update(DictationaryDataItem dictationaryDataItem);
    Task<DictationaryDataItem> GetByIdAsync(int id);
   
}