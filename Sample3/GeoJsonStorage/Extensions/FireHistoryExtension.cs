
using GeoJsonConvertor.Core;
using GeoJsonConvertor.Models;
using GeoJsonStorage;

namespace GeoJsonConvertor.Extensions;


public static class RegionConvertExtension
{
    /// <summary>
    /// Сконвертировать модель в параметр SQL запроса
    /// </summary>
    /// <param name="source"></param>
    /// <param name="convertType"> Тип конвертиации: заголовок или данные </param>
    /// <returns></returns>
    public static string Convert<T>(this IModel source, ConvertType convertType) where T:FireHistory
    {
        var data = source as T ?? throw new InvalidOperationException($"Невозожно сконвертировать переданную модель {source} в указанный тип данных {typeof(T)}");
        var value = convertType switch
        {
            ConvertType.Head => $"id, region_id, year, month, period",
            ConvertType.Data => $"'{source.Id}', '{data.RegionId}', {data.Period.Year}, {data.Period.Month}, '{data.Period.ToString("yyyy-MM-dd")}' ",
            ConvertType.TableName => $"public.fire_history",
            _ => throw new NotImplementedException()
        };
        return value;
    }
}