using System.Collections;
using System.Reflection;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate;
using Catalogs.Domain.AggregateModel.Common;
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
     /*
     public IReadOnlyCollection<DecimalAttributeDescription> DecimalAttributeDescriptions => _decimalAttributeDescriptions.AsReadOnly();
      public IReadOnlyCollection<StringAttributeDescription> StringAttributeDescriptions => _stringAttributeDescriptions.AsReadOnly();
      public IReadOnlyCollection<CatalogRecordItemAttributeDescription> CatalogRecordItemAttributeDescriptions => _catalogRecordItemAttributeDescriptions.AsReadOnly();
      */

    protected CatalogItem()
    {
        _intAttributeDescriptions = new List<IntAttributeDescription>();
        _stringAttributeDescriptions = new List<StringAttributeDescription>();
        _decimalAttributeDescriptions = new List<DecimalAttributeDescription>();
        _catalogRecordItemAttributeDescriptions = new List<CatalogRecordItemAttributeDescription>();
         
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
    
    public void AddAttribute(IAttributeDescription attributeDescription)
    { 
        var descriptionsField = GetAttributeDescriptionsFieldByType(attributeDescription.Type);
        
        if (descriptionsField == null)
            throw new CatalogDomainException($"Attribute with type {attributeDescription.AttributeType} not supported.");
        
        var existedDescription = descriptionsField
            .FirstOrDefault(d => d.AttributeName == attributeDescription.AttributeName);
     
        if (existedDescription != null)
            throw new CatalogDomainException($"Attribute with name {attributeDescription.AttributeType} already exists.");
      
        (descriptionsField as IList).Add(attributeDescription);
        
       // attributeDescription.CatalogItemId = this.Id;
        
        this.AddDomainEvent(new AttributeDescriptionAddedEvent(attributeDescription));
    }
    public void DeleteAttribute(IAttributeDescription attributeDescription)
    { 
        var descriptionsField = GetAttributeDescriptionsFieldByType(attributeDescription.Type);
        
        if (descriptionsField == null)
            throw new CatalogDomainException($"Attribute with type {attributeDescription.AttributeType} not supported.");

        var existedDescription = descriptionsField
            .FirstOrDefault(d => d.AttributeName == attributeDescription.AttributeName);
     
        if (existedDescription == null)
            throw new CatalogDomainException($"Attribute with name {attributeDescription.AttributeName} not founded.");

        (descriptionsField as IList).Remove(existedDescription);
        
        this.AddDomainEvent(new AttributeDescriptionDeletedEvent(existedDescription));
    }
    public void AddAttribute(IAttribute attribute)
    {
        var newDescription = (IAttributeDescription) Activator.CreateInstance(attribute.DescriptionType, new object[] { attribute  })!;

        this.AddAttribute(newDescription);
    }
    public void DeleteAttribute(IAttribute attribute)
    {
     
        var descriptionsField = GetAttributeDescriptionsFieldByType(attribute.DescriptionType);
        
        var moskDescription = (IAttributeDescription) Activator.CreateInstance(attribute.DescriptionType, new object[] { attribute  })!;
        
        this.DeleteAttribute(moskDescription);
       
    }

    public IEnumerable<IEnumerable<ValueTuple<Type,IAttributeDescription>>> GetAllAttributeDescriptionsFields()
    {  
        var AllProps = new List<List<ValueTuple<Type,IAttributeDescription>>>(); 

        var attrDescriptionFields = this.GetType().GetFields(BindingFlags.NonPublic |
                                                             BindingFlags.Instance |
                                                             BindingFlags.Static)
            .Where(x =>
                x.FieldType.Name == "List`1" &&
                x.FieldType.GetGenericArguments()[0].GetInterface("IAttributeDescription") != null);
            //.Select(p=>AllProps.Add(p.GetValue(this) as List<IAttributeDescription>));
    
        foreach (var fieldInfo in attrDescriptionFields)
        {
            var type = fieldInfo.FieldType.GetGenericArguments()[0];
            var props = (fieldInfo.GetValue(this) as List<IAttributeDescription>)
                .Select(a=> new  {a.Type,a});
             
           // AllProps.Add(props);
           }    
        return AllProps;
    }
    
    private IEnumerable<IAttributeDescription> GetAttributeDescriptionsFieldByType(Type descriptionType)
    {
        /*
        var f = this.GetType().GetFields(BindingFlags.NonPublic |
                                         BindingFlags.Instance |
                                         BindingFlags.Static);
                                         */
        
        var attrDescriptionField = this.GetType().GetFields(BindingFlags.NonPublic |
                                                            BindingFlags.Instance |
                                                            BindingFlags.Static)
            .FirstOrDefault(x =>
                x.FieldType.Name == "List`1" && x.FieldType.GetGenericArguments()[0] == descriptionType)
                ?.GetValue(this);
       
        return attrDescriptionField  as IEnumerable<IAttributeDescription>; 
    }

}

