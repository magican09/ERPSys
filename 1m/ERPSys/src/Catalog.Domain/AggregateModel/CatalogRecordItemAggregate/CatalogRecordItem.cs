
 

using System.Collections;
using System.Reflection;
using System.Text.Json;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.Events;
using Catalogs.Domain.Exceptions;

namespace Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;

public class CatalogRecordItem:Entity,IAggregateRoot
{
    public int CatalogItemId { get; private set; }
   
    private List<IntAttribute> _intAttributes;
    private List<StringAttribute> _stringAttributes;
    private List<DecimalAttribute> _decimalAttributes;
    private List<CatalogRecordItemAttribute> _catalogRecordItemAttributes;
    public IReadOnlyCollection<IntAttribute> IntAttributes => _intAttributes.AsReadOnly();
    
    public IReadOnlyCollection<StringAttribute> StringAttributes => _stringAttributes.AsReadOnly();
    public IReadOnlyCollection<DecimalAttribute> DecimalAttributes => _decimalAttributes.AsReadOnly();
    public IReadOnlyCollection<CatalogRecordItemAttribute> CatalogRecordItemAttributes => _catalogRecordItemAttributes.AsReadOnly();
   
    public  Dictionary<Type, IEnumerable<IAttribute>> AttributesMap { get; init; } = new Dictionary<Type, IEnumerable<IAttribute>>();
    
    public CatalogRecordItem()
    {
        _intAttributes = new List<IntAttribute>();
        _decimalAttributes = new List<DecimalAttribute>();
        _stringAttributes = new List<StringAttribute>();
        _catalogRecordItemAttributes = new List<CatalogRecordItemAttribute>();
      
        AttributesMap.Add(typeof(IntAttribute),_intAttributes);
        AttributesMap.Add(typeof(DecimalAttribute),_decimalAttributes );
        AttributesMap.Add(typeof(StringAttribute),_stringAttributes );
        AttributesMap.Add(typeof(CatalogRecordItemAttribute),_catalogRecordItemAttributes );

        
    }

    public CatalogRecordItem(CatalogItem catalog):this()
    {
        CatalogItemId = catalog.Id;
        
        /*foreach (var attributeDescription in catalog.IntAttributeDescriptions)
        {
            var newAttribute =  new IntAttribute(attributeDescription.AttributeName);
            this.CreateAttribute(newAttribute);
        }*/
    }
 

    public void DeleteAttribute(Type attributeType, string attributeName)
    {
        var attribute = AttributesMap[attributeType].FirstOrDefault(a=>a.Name == attributeName);

        if (attribute == null)
            throw new CatalogDomainException(
                $"Attribute by type {attributeType.FullName}  or name {attributeName} is not found");
        
        var attributes = AttributesMap[attributeType];
        
        (attributes as IList).Remove(attributes);
        
        this.AddDomainEvent(new AttributeDeletedEvent(attribute));
    }

    public void DeleteAttribute(string attributeTypeClassName, string attributeName)
    {
        var attributeType = DomainHelpers.GetTypeByClassName(attributeTypeClassName);
        
        this.DeleteAttribute(attributeType, attributeName);
    }
    
    public void UpdateAttribute(Type attributeType, string attributeName, object attributeValue)
    {
        var attributeValueType = attributeType.GenericTypeArguments.First();
      
        object attrbNewValue;
        
        try
        {
            attrbNewValue = Convert.ChangeType(attributeValue, attributeValueType);
        }
        finally
        {
            attrbNewValue = attributeValue;
        }
        
         
        
        var attribute = AttributesMap[attributeType].FirstOrDefault(a=>a.Name == attributeName);
      
        if (attribute == null)
            throw new CatalogDomainException(
                $"Attribute by type {attributeType.FullName}  or name {attributeName} is not found");

        attribute.SetValue(attrbNewValue);
    }
    public void UpdateAttribute(string attributeTypeClasssName, string attributeName, object attributeValue)
    {
        var attributeType = DomainHelpers.GetTypeByClassName(attributeTypeClasssName);
        
        this.UpdateAttribute(attributeType, attributeName, attributeValue);
    }
    
    public IAttribute ReadAttribute(Type attributeType, string attributeName)
    {
        var attribute = AttributesMap[attributeType].FirstOrDefault(a=>a.Name == attributeName);
       
        if (attribute == null)
            throw new CatalogDomainException(
                $"Attribute by type {attributeType.FullName}  or name {attributeName}is not found");

        return attribute;
    }

    public IAttribute ReadAttribute(string attributeClassName, string attributeName)
    {
        var attributeType = DomainHelpers.GetTypeByClassName(attributeClassName);
        
        return ReadAttribute(attributeType, attributeName);
    }
    
    
    public void CreateAttribute(Type attributeType, string attributeName, object attributeValue)
    {
        var attrValue = Convert.ChangeType(attributeValue, attributeType);
        
        var newAttribute = (IAttribute) Activator.CreateInstance(attributeType, new object[] { attributeName,attrValue});
     
        if (newAttribute == null)
            throw new CatalogDomainException(
                $"Attribute by type {attributeType.FullName} not supported");

        var attributes = AttributesMap.FirstOrDefault(a=>a.Key == attributeType).Value; 
       
        (attributes as IList).Add(newAttribute);
        
        this.AddDomainEvent(new AttributeAddedEvent(newAttribute));
    }

    public void CreateAttribute(string attributeClassName, string attributeName, object attributeValue)
    {
        var classType = DomainHelpers.GetTypeByClassName(attributeClassName);
        
        this.CreateAttribute(classType, attributeName, attributeValue);
        
    }

}