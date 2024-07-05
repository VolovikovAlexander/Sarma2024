using System.Reflection;
using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Extensions;

public static class StringArrayExtension
{
    public static  CommandLineArgs GetProperties(this string[] source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));  
        if(source.Length == 0) throw new ArgumentException("Набор аргументов пуст!");
        var result = new CommandLineArgs();
        var items = result.GetType()
                    .GetCustomAttributes<PositionArgsAttribute>()
                    .Select(X => new {
                        Key = X.
                    } )





    }
}