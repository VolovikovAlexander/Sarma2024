using System.Reflection;
using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Extensions;

public static class StringArrayExtension
{
    /// <summary>
    /// Обработать аргументы и вернуть структуру с параметрами
    /// </summary>
    /// <param name="source"> Исходный набор аргументов </param>
    /// <returns></returns>
    public static  CommandLineArgs GetProperties(this string[] source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));  
        if(source.Length == 0) throw new ArgumentException("Набор аргументов пуст!");
        var result = new CommandLineArgs();
        var items = result.GetType().GetProperties()
                    .Select(x => new {
                        Key = x,
                        Value =  new Tuple<int, Type>( x.GetCustomAttribute<PositionArgsAttribute>()?.Position ?? 1, 
                                                       x.GetCustomAttribute<PositionArgsAttribute>()?.Type ?? typeof(string))
                    }).ToDictionary(x => x.Key, x => x.Value);

        foreach(var item in items.Keys)
        {
            var position = items[item].Item1;
            var destType = items[item].Item2;
            try
            {
                var value = Convert.ChangeType( source[position - 1] , destType);
                item.SetValue(result, value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Позиция {position}. Значение {source[position]}. Формат {destType}. Некорректный формат данных!\n{ex}");
            }
        }

        return result;
    }
}