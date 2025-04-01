
 

namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;

public class CatalogRecordItem:Entity,IAggregateRoot
{
    /*
    private List<IntAttribute> _intAttributes;
    private List<StringAttribute> _stringAttributes;
    private List<DecimalAttribute> _decimalAttributes;
    private List<CatalogRecordItemAttribute> _catalogRecordItemAttributes;
    public IReadOnlyCollection<IntAttribute> IntAttributes => _intAttributes.AsReadOnly();
    public IReadOnlyCollection<StringAttribute> StringAttributes => _stringAttributes.AsReadOnly();
    public IReadOnlyCollection<DecimalAttribute> DecimalAttributes => _decimalAttributes.AsReadOnly();
    public IReadOnlyCollection<CatalogRecordItemAttribute> CatalogRecordItemAttributes => _catalogRecordItemAttributes.AsReadOnly();
    */

   // public static  List<AttributeDescription> AttributeDescriptions { get; } = new List<AttributeDescription>();

   private List<IAttribute> _attributes;
   
   public IReadOnlyList<IAttribute> Attributes => _attributes.AsReadOnly();
    public CatalogRecordItem()
    {
        _attributes = new List<IAttribute>();
        /*_intAttributes = new List<IntAttribute>();
        _decimalAttributes = new List<DecimalAttribute>();
        _stringAttributes = new List<StringAttribute>();
        _catalogRecordItemAttributes = new List<CatalogRecordItemAttribute>();*/
    }
}