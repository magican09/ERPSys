using DictationaryDataService.Domain.SeedWork;

namespace DictationaryDataService.Infrastructure.Repositories;

public class DictationaryDataRepository:IDictationaryDataRepository
{
    private DictationaryDataSertviceContext _context;
    
    public IUnitOfWork UnitOfWork  => _context;
    
    
    public DictationaryDataRepository(DictationaryDataSertviceContext dbContext)
    {
        _context = dbContext;
    }
    public DictationaryData Add(DictationaryData dictationaryData)
    {
     return _context.DictationaryDatas.Add(dictationaryData).Entity;
    }

    public void Update(DictationaryData dictationaryData)
    {
       _context.Entry(dictationaryData).State = EntityState.Modified;
    }

    public async Task<DictationaryData> GetAsync(int dictationaryId)
    {
        var dictationaryData = await _context.DictationaryDatas.FindAsync(dictationaryId);

        if (dictationaryData != null)
        {
            await _context.Entry(dictationaryData)
                .Collection(i => i.RequisiteProperties).LoadAsync();
        }
        return dictationaryData;

    }

   
}