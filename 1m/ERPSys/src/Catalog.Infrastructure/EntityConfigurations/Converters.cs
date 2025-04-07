using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.Infrastructure.EntityConfigurations;

public class StringToTypeConverter:ValueConverter<Type, string>
{
    public StringToTypeConverter() 
        : base( 
            v=>v.ToString(),
            v=>ConverterHelpers.GetConvertedType(v))
    {
        
    }
}

public static class ConverterHelpers
{
    public static Type GetConvertedType(string value)
    {
        
        return System.Type.GetType(value);
    }
}