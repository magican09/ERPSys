using System.Collections;
using System.Reflection;

namespace Catalogs.Domain;

public static class DomainHelpers
{ 
    public static  IList<Type> DomainAssebliesTypes => Assembly.GetAssembly(typeof(DomainHelpers)).ExportedTypes.ToList();
    
    public static  Type GetTypeByClassName(string classsName) => DomainAssebliesTypes.FirstOrDefault(t => t.Name.EndsWith(classsName));

    public static IEnumerable GetFieldByType(object entity,Type fildType)
    {
        var entityType = entity.GetType();
      
        var entityField =entityType.GetFields(BindingFlags.NonPublic |
                                             BindingFlags.Instance |
                                             BindingFlags.Static)
            .FirstOrDefault(x =>
                x.FieldType.Name == "List`1" && x.FieldType.GetGenericArguments()[0] == fildType)
            ?.GetValue(entity);
       
        return entityField  as IEnumerable; 
    }
    

}