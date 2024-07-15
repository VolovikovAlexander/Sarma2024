
using GeoJsonConvertor.Core;
using GeoJsonConvertor.Models;
using GeoJsonStorage;

namespace GeoJsonConvertor.Extensions;


public static class IModelExtension
{
    /// <summary>
    /// Сконвертировать модель в параметр SQL запроса
    /// </summary>
    /// <param name="source"></param>
    /// <param name="convertType"> Тип конвертиации: заголовок или данные </param>
    /// <returns></returns>
    public static string Convert(this IModel source, ConvertType convertType) 
    {
        if(source.GetType() == typeof(Region)) 
        {
            var region = source as Region;
            return Convert(region!, convertType);
        }

        if(source.GetType() == typeof(FireHistory)) 
        {
            var fireHistory = source as FireHistory;
            return Convert(fireHistory!, convertType);
        }

        return string.Empty;
    }


    /// <summary>
    /// Сконвертировать модель в параметр SQL запроса
    /// </summary>
    /// <param name="source"></param>
    /// <param name="convertType"> Тип конвертации: заголовок или данные </param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception> <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="convertType"></param>
    /// <returns></returns>
    private static string Convert(Region source, ConvertType convertType)
    {
        var value = convertType switch
        {
            ConvertType.Head => $"id, name",
            ConvertType.Data => $"'{source.Id}', '{source.Name}'",
            ConvertType.TableName => $"public.regions",
            _ => throw new NotImplementedException()
        };
        return value;
    }


    /// <summary>
    /// Сконвертировать модель в параметр SQL запроса
    /// </summary>
    /// <param name="source"></param>
    /// <param name="convertType"> Тип конвертиации: заголовок или данные </param>
    /// <returns></returns>
    private static string Convert(FireHistory source, ConvertType convertType) 
    {
        var value = convertType switch
        {
            ConvertType.Head => $"id, region_id, year, month, period",
            ConvertType.Data => $"'{source.Id}', '{source.RegionId}', {source.Period.Year}, {source.Period.Month}, '{source.Period.ToString("yyyy-MM-dd")}' ",
            ConvertType.TableName => $"public.fire_history",
            _ => throw new NotImplementedException()
        };
        return value;
    }


}