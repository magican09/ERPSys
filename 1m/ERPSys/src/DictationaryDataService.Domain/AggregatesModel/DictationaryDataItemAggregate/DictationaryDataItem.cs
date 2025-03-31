
 
namespace DictationaryDataService.Domain.AggregatesModel.DictationaryDataItemAggregate;

public class DictationaryDataItem:Entity,IAggregateRoot
{
    private List<IntRequisite> _intRequisites;
    private List<StringRequisite> _stringRequisites;
    private List<DecimalRequisite> _decimalRequisites;
    private List<DictationaryDataItemRequisite> _dictationaryDataItemsRequisites;

    public int DictationaryDataId { get; private set; }
    //public DictationaryData DictationaryData { get; private set; }
    
    public IReadOnlyCollection<IntRequisite> IntRequisites => _intRequisites.AsReadOnly();
    public IReadOnlyCollection<StringRequisite> StringRequisites => _stringRequisites;
    public IReadOnlyCollection<DecimalRequisite> DecimalRequisites => _decimalRequisites;
    public IReadOnlyCollection<DictationaryDataItemRequisite> DictationaryDataItemsRequisites => _dictationaryDataItemsRequisites.AsReadOnly();

    protected DictationaryDataItem()
    {
        _intRequisites = new List<IntRequisite>();
        _decimalRequisites = new List<DecimalRequisite>();
        _stringRequisites = new List<StringRequisite>();
        _dictationaryDataItemsRequisites = new List<DictationaryDataItemRequisite>();
    }

    /*public void AddRequisite(string name, object? value)
    {
        
        
        var requisiteField = this.GetType().GetFields(BindingFlags.Instance|
                                                      BindingFlags.NonPublic|
                                                      BindingFlags.Static)
            .FirstOrDefault(x => x.Name == name);
    }*/
    public void AddIntRequisite(string name,int value)
    {
        _intRequisites.Add(new IntRequisite(name, value));
    }

    public void AddStringRequisite(string name, string value)
    {
        _stringRequisites.Add(new StringRequisite(name, value));
    }

    public void AddDecimalRequisite(string name, decimal value)
    {
        _decimalRequisites.Add(new DecimalRequisite(name, value));
    }

    public void AddDictqationaryDataItemRequisite(string name, DictationaryDataItemRequisite value)
    {
        _dictationaryDataItemsRequisites.Add(value);
    }
}