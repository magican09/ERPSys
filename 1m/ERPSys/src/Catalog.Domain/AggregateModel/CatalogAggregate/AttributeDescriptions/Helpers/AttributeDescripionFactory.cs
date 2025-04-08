using System.Reflection;
using Catalogs.Domain.Exceptions;

namespace Catalogs.Domain.AggregateModel.CatalogAggregate.AttributeDescriptions;

public static class AttributeDescripionFactory
{
    public static IAttributeDescription  CreateAttributeDescription(string atributeClassName)
    {
        var assemlyTypes = DomainHelpers.DomainAssebliesTypes; 
            
        var atrDescrClassFullName = assemlyTypes
            .SelectMany(t => t.GetProperties())
            .Where(p => p.Name == "AttributeType")
            .FirstOrDefault(p=>p.DeclaringType.IsConstructedGenericType 
                               && p.DeclaringType.GetGenericArguments()[0].Name == atributeClassName)
            .ReflectedType.AssemblyQualifiedName;
      
        if(atrDescrClassFullName == null)
            throw new CatalogDomainException($"Attribute with name {atributeClassName} not supported.");
        
        var atrDescrType = System.Type.GetType(atrDescrClassFullName);
     
        if(atrDescrType == null)
            throw new CatalogDomainException($"Attribute  type {atrDescrClassFullName}  don't created.");
        
        var  newAtrbDescription = (IAttributeDescription) Activator.CreateInstance(atrDescrType,new object[] { })!;
      
        if(newAtrbDescription == null)
            throw new CatalogDomainException($"New attribute  object {atrDescrClassFullName} not created.");
        
        return newAtrbDescription;

    }
    
}