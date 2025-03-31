namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate;

public interface IDictationaryDataRepository:IRepository<DictationaryData>
{
    DictationaryData Add(DictationaryData dictationaryData);
    void Update(DictationaryData dictationaryData);
    Task<DictationaryData> GetAsync(int dictationaryId);
 
}