
using GeoJsonConvertor.Core;
using GeoJsonConvertor.Models;
using GeoJsonStorage;

namespace GeoJsonConvertor.Extensions;


public static class RegionExtension
{
    /// <summary>
    /// Сконвертировать модель в параметр SQL запроса
    /// </summary>
    /// <param name="source"></param>
    /// <param name="convertType"> Тип конвертиации: заголовок или данные </param>
    /// <returns></returns>
    public static string Convert<T>(this IModel source, ConvertType convertType) where T: Region
    {
        var data = source as T ?? throw new InvalidOperationException($"Невозожно сконвертировать переданную модель {source} в указанный тип данных {typeof(T)}");
        var value = convertType switch
        {
            ConvertType.Head => $"id, name",
            ConvertType.Data => $"'{data.Id}', '{data.Name}'",
            ConvertType.TableName => $"public.regions",
            _ => throw new NotImplementedException()
        };
        return value;
    }

}