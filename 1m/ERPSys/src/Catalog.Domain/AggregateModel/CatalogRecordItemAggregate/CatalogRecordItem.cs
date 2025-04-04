
 

using System.Collections;
using System.Reflection;
using Catalogs.Domain.Events;

namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;

public class CatalogRecordItem:Entity,IAggregateRoot
{
    public int CatalogItemId { get; private set; }
   
    private List<IntAttribute> _intAttributes;
    private List<StringAttribute> _stringAttributes;
    private List<DecimalAttribute> _decimalAttributes;
    private List<CatalogRecordItemAttribute> _catalogRecordItemAttributes;
    public IReadOnlyCollection<IntAttribute> IntAttributes => _intAttributes.AsReadOnly();
    /*
    public IReadOnlyCollection<StringAttribute> StringAttributes => _stringAttributes.AsReadOnly();
    public IReadOnlyCollection<DecimalAttribute> DecimalAttributes => _decimalAttributes.AsReadOnly();
    public IReadOnlyCollection<CatalogRecordItemAttribute> CatalogRecordItemAttributes => _catalogRecordItemAttributes.AsReadOnly();
   */
   public CatalogRecordItem()
    {
        _intAttributes = new List<IntAttribute>();
        _decimalAttributes = new List<DecimalAttribute>();
        _stringAttributes = new List<StringAttribute>();
        _catalogRecordItemAttributes = new List<CatalogRecordItemAttribute>();
    }

    public void AddAttribute(IAttribute attribute)
    {
        var attributesField = GetAttributesFieldByType(attribute.Type);
        
        if(attributesField == null)
            throw new Exception($"Attribute type {attribute.Type.Name} is not supported");
       
        var existingAttribute = attributesField.FirstOrDefault(a=>a.Name == attribute.Name);

        if (existingAttribute != null)
            throw new Exception($"Attribute {attribute.Name} already exists");
        
        var newAtribute = (IAttribute)Activator.CreateInstance(attribute.Type, new object[] { attribute.Name});

        (attributesField as IList).Add(newAtribute);
        
        this.AddDomainEvent(new AttributeAddedEvent(newAtribute));
    }
 
    public void DeleteAttribute(IAttribute attribute)
    {
        var attributesField = GetAttributesFieldByType(attribute.Type);
        
        if(attributesField == null)
            throw new Exception($"Attribute type {attribute.Type.Name} is not supported");
       
        var existingAttribute = attributesField.FirstOrDefault(a=>a.Name == attribute.Name);

        if (existingAttribute == null)
            throw new Exception($"Attribute {attribute.Name} not found");
        
       
        (attributesField as IList).Remove(attribute);
        
        this.AddDomainEvent(new AttributeAddedEvent(existingAttribute));
    }
    private IEnumerable<IAttribute> GetAttributesFieldByType(Type attributreType)
    {
        var f = this.GetType().GetFields(BindingFlags.NonPublic |
                                         BindingFlags.Instance |
                                         BindingFlags.Static);
        
        var attrsField = this.GetType().GetFields(BindingFlags.NonPublic |
                                                            BindingFlags.Instance |
                                                            BindingFlags.Static)
            .FirstOrDefault(x =>
                x.FieldType.Name == "List`1" && x.FieldType.GetGenericArguments()[0] == attributreType)
            ?.GetValue(this);
       
        return attrsField  as IEnumerable<IAttribute>; 
    }
}