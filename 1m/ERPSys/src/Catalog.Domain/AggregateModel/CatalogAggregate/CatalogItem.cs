using System.Collections;
using System.Reflection;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using Catalogs.Domain.AggregateModel.Common;
using Catalogs.Domain.Events;
using Catalogs.Domain.Events.AttributeDescriptionEvents;
using Catalogs.Domain.Exceptions;

namespace Catalogs.Domain.AggregateModel.CatalogAggregate;

public class CatalogItem:Entity,IAggregateRoot
{
   
    public string Name { get; private set; }
    public string? Synonym  { get; private set; }
    public  bool UseStandardCommands { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public string? CreateOnInput { get; private set; }
    public string? DataLockControlMode { get; private set; }
    public string? FullTextSearch { get; private set; }
    public int LevelCount { get; private set; }
    public bool FoldersOnTop { get; private set; }
    public int CodeLength { get; private set; }
    public int DescriptionLength { get; private set; }
    public string? CodeType  { get; private set; }
    public int CodeAllowedLength { get; private set; }
    public bool CheckUnique { get; private set; }
    public bool Autonumbering { get; private set; }
    public string? DefaultPresentation { get; private set; }
    public string? EditType { get; private set; }
    public string? ChoiceMode { get; private set; }
    
    private List<IntAttributeDescription>  _intAttributeDescriptions;
    private List<StringAttributeDescription> _stringAttributeDescriptions;
    private List<DecimalAttributeDescription> _decimalAttributeDescriptions;
    private List<CatalogRecordItemAttributeDescription> _catalogRecordItemAttributeDescriptions;
    public IReadOnlyCollection<IntAttributeDescription> IntAttributeDescriptions => _intAttributeDescriptions.AsReadOnly();
    public IReadOnlyCollection<DecimalAttributeDescription> DecimalAttributeDescriptions => _decimalAttributeDescriptions.AsReadOnly();
    public IReadOnlyCollection<StringAttributeDescription> StringAttributeDescriptions => _stringAttributeDescriptions.AsReadOnly();
   public IReadOnlyCollection<CatalogRecordItemAttributeDescription> CatalogRecordItemAttributeDescriptions => _catalogRecordItemAttributeDescriptions.AsReadOnly();
     
    public Dictionary<Type,IEnumerable<IAttributeDescription>> AttributeDescriptionsMap { get; init; } = new Dictionary<Type,IEnumerable<IAttributeDescription>>();
    protected CatalogItem()
    {
        _intAttributeDescriptions = new List<IntAttributeDescription>();
        _stringAttributeDescriptions = new List<StringAttributeDescription>();
        _decimalAttributeDescriptions = new List<DecimalAttributeDescription>();
        _catalogRecordItemAttributeDescriptions = new List<CatalogRecordItemAttributeDescription>();
        
        AttributeDescriptionsMap.Add(typeof(IntAttributeDescription), _intAttributeDescriptions);
        AttributeDescriptionsMap.Add(typeof(StringAttributeDescription), _stringAttributeDescriptions);
        AttributeDescriptionsMap.Add(typeof(DecimalAttributeDescription), _decimalAttributeDescriptions);
        AttributeDescriptionsMap.Add(typeof(CatalogRecordItemAttributeDescription), _catalogRecordItemAttributeDescriptions);
    }

    public CatalogItem(string name):this()
    {
        Name = name;
    }

    public CatalogItem(
     string name,
     string synonym ,
     
     string code ,
     string? codeType ,
     int codeLength ,
     int codeAllowedLength ,
     
     string description ,
     int descriptionLength,
     
     string createOnInput, 
     string dataLockControlMode,
     string fullTextSearch ,
     int levelCount ,
     bool foldersOnTop ,
    
     bool checkUnique ,
     bool autonumbering ,
     string defaultPresentation ,
     string editType,
     string choiceMode,
    bool useStandardCommands ):this()
    {
        Name = name;
        Synonym = synonym;
        UseStandardCommands = useStandardCommands;
        Code = code;
        Description = description;
        CreateOnInput = createOnInput;
        DataLockControlMode = dataLockControlMode;
        FullTextSearch = fullTextSearch;
        LevelCount = levelCount;
        FoldersOnTop = foldersOnTop;
        CodeLength = codeLength;
        DescriptionLength = descriptionLength;
        CodeType = codeType;
        CodeAllowedLength = codeAllowedLength;
        CheckUnique = checkUnique;
        Autonumbering = autonumbering;
        DefaultPresentation = defaultPresentation;
        EditType = editType;
        ChoiceMode = choiceMode;
        
    }
    

    public IAttributeDescription CreateAttributeDescription(Type attributeType, string attributeName, Dictionary<string,object> attributeProperties)
    {
        var attributeDescriptionType = GetAttributeDescriptionType_ByAttributeType(attributeType);
      
        var newAttributeDescription = (IAttributeDescription) Activator.CreateInstance(attributeDescriptionType , new object[]{attributeName});
      
        if(newAttributeDescription==null)
            new CatalogDomainException($"Attribute with type {attributeType} could not be instantiated.");

        SetAttributeDescriptionProperties(newAttributeDescription, attributeProperties);
        
        var attributeDescriptions = AttributeDescriptionsMap[attributeDescriptionType];
        
        if(attributeDescriptions.FirstOrDefault(ad=>ad.AttributeName==attributeName) != null)
            throw new CatalogDomainException($"Attribute with name {attributeName} already exists.");
        
        (attributeDescriptions as IList).Add(newAttributeDescription);
        
        this.AddDomainEvent(new AttributeDescriptionAddedEvent(newAttributeDescription));
        return newAttributeDescription;
    }

    public void CreateAttributeDescription(string attributeTypeClassName, string attributeName, Dictionary<string,object> attributeProperties)
    {
        var attributeDescriptionType = DomainHelpers.GetTypeByClassName(attributeTypeClassName);
        this.CreateAttributeDescription(attributeDescriptionType, attributeName, attributeProperties);
    }

    public void AddAttributeDescription(IAttributeDescription attributeDescription)
    {
        var existingAttributeDescription = AttributeDescriptionsMap[attributeDescription.Type]
            .FirstOrDefault(ad=>ad.AttributeName == attributeDescription.AttributeName);
          
        if(existingAttributeDescription != null)
            throw new CatalogDomainException($"Attribute with name {attributeDescription.AttributeName} already exists.");
       
        var attributeDescriptions = AttributeDescriptionsMap[attributeDescription.Type];
        
        (attributeDescriptions as IList).Add(attributeDescription);
        
        this.AddDomainEvent(new AttributeDescriptionAddedEvent(attributeDescription));
    }
    public IAttributeDescription ReadAttributeDescription(Type attributeType, string attributeName)
    {
        var attributeDescriptionType = GetAttributeDescriptionType_ByAttributeType(attributeType);
        
        var attributeDescription = AttributeDescriptionsMap[attributeDescriptionType]
            .FirstOrDefault(x => x.AttributeName == attributeName);
        
        if(attributeDescription==null)
            throw new CatalogDomainException($"Attribute with name {attributeName} could not be found.");
        
        return attributeDescription;
    }

    public IAttributeDescription ReadAttributeDescription(string attributeTypeClassName ,string attributeName)
    {
        return this.ReadAttributeDescription(Type.GetType(attributeTypeClassName), attributeName);
    }

    public void UpdateAttributeDescription(Type attributeType, string attributeName,
        Dictionary<string, object> attributeProperties)
    {
        var attributeDescriptionType = GetAttributeDescriptionType_ByAttributeType(attributeType);
      
        var attributeDescription = AttributeDescriptionsMap[attributeDescriptionType]
            .FirstOrDefault(x => x.AttributeName == attributeName);
      
        if(attributeDescription==null)
            new CatalogDomainException($"Attribute with type {attributeType} or name {attributeName} could not be found.");

        SetAttributeDescriptionProperties(attributeDescription, attributeProperties);
      
        this.AddDomainEvent(new AttributeDescriptionUpdatedEvent(attributeDescription));
    }

    public void UpdateAttributeDescription(string attributeClassName, string attributeName,
        Dictionary<string,object> attributeProperties)
    {
        var attributeDescriptionType = DomainHelpers.GetTypeByClassName(attributeClassName);
        
        this.UpdateAttributeDescription(attributeDescriptionType, attributeName, attributeProperties);
    }

    public void DeleteAttributeDescription(Type attributeType, string attributeName)
    {
        var attributeDescriptionType = GetAttributeDescriptionType_ByAttributeType(attributeType);
        
        var attributeDescription = AttributeDescriptionsMap[attributeDescriptionType]
            .FirstOrDefault(x => x.AttributeName == attributeName);
        
        if(attributeDescription==null)
            new CatalogDomainException($"Attribute with type {attributeType} or name {attributeName} could not be found.");
      
        var attributeDescriptions = AttributeDescriptionsMap[attributeDescriptionType];
        
        (attributeDescriptions as IList).Remove(attributeDescription);
        
        this.AddDomainEvent(new AttributeDescriptionDeletedEvent(attributeDescription));
    }

    public void DeleteAttributeDescription(string attributeClassName, string attributeName)
    {
        var attributeDescriptionType = DomainHelpers.GetTypeByClassName(attributeClassName);
        this.DeleteAttributeDescription(attributeDescriptionType, attributeName);
    }

    public static void SetAttributeDescriptionProperties(IAttributeDescription attributeDescription,
        Dictionary<string,  object> attributeProperties)
    {
        var attributeDescriptionType = attributeDescription.GetType();
        
        foreach (var property in attributeProperties)
        {
            
            var proprtyName = property.Key;
            
            var atrbDecriptionPropInfo =   attributeDescriptionType
                .GetProperties()
                .FirstOrDefault(p=>p.Name == proprtyName && p.CanWrite);

            object proprtyValue;
            try
            {
                proprtyValue = Convert.ChangeType(property.Value, atrbDecriptionPropInfo.PropertyType);
            }
            finally
            {
                proprtyValue = property.Value;
            }
          
              
            if(atrbDecriptionPropInfo == null)
                throw new CatalogDomainException($"Attribute with proiperty {proprtyName} could not be found or does not have a setter.");
                      
            atrbDecriptionPropInfo.SetValue(attributeDescription, proprtyValue);
        }
    }

    public static Dictionary<string,  object> GetAttributeDescriptionProperties(IAttributeDescription attributeDescription)
    {
        var attributeDescriptionType = attributeDescription.GetType();
       
        var attributeProperties = new Dictionary<string, object>();
        
        var attrDescriptionPropertyInfos = attributeDescriptionType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        
        foreach (var propInfo in attrDescriptionPropertyInfos)
        {
            var proprtyName = propInfo.Name;
            var propertyValue =  propInfo.GetValue(attributeDescription);
            attributeProperties.Add(proprtyName, propertyValue);
       }
        
        return attributeProperties;
    }
    
    private Type GetAttributeDescriptionType_ByAttributeType(Type attributeType)
    {
        var attributeDescriptionType = AttributeDescriptionsMap
            .FirstOrDefault(ad => ad.Key.GetGenericArguments()[0] == attributeType).Key;
      
        if(attributeDescriptionType==null)
            new CatalogDomainException($"Attribute with type {attributeType} not found.");
        
        return attributeDescriptionType;
    }
}

