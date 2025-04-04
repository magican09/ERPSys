using System.ComponentModel;
using Catalogs.Domain.Exceptions;

namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public static class AttributeDescriptionHelper
{
 public static void SetAttributeDescriptionProperties(IAttributeDescription attributeDescription,
  Dictionary<string, string> propNameValuePairs)
 {
  var allDescrPropsInfos = attributeDescription.GetType().GetProperties(); 
  
  var descrPropsInfos = allDescrPropsInfos.Where(p=>propNameValuePairs.ContainsKey(p.Name));

  var notExistedProps = propNameValuePairs
   .Where(kvp => allDescrPropsInfos.FirstOrDefault(pi=>pi.Name==kvp.Key) == null);
  
  foreach (var propName in notExistedProps)
  {
    throw new CatalogDomainException($"Property {propName} is not existed in type {attributeDescription.GetType().FullName} attribute.");
  }
  
  foreach (var descrPropInfo in descrPropsInfos)
  {
   var descrPropType = descrPropInfo.PropertyType;
   
   var dictPropValueStr = propNameValuePairs.FirstOrDefault(p => p.Key == descrPropInfo.Name).Value;
   
   object dictPropValue = null;
   try
   {
    dictPropValue = Convert.ChangeType(dictPropValueStr, descrPropType);
    
    if(descrPropInfo.CanWrite)
     descrPropInfo.SetValue(attributeDescription, dictPropValue);
   }
   catch (Exception e)
   {
     throw new CatalogDomainException($"Attribute {attributeDescription.AttributeType} value of property {descrPropInfo.Name} has not resolver because of type mismatch.");
   }
    
   
   }
  
 }
}