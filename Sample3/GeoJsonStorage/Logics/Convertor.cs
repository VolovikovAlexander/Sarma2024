using System.Data;
using System.Text.Json;
using GeoJsonConvertor.Core;
using GeoJsonConvertor.Extensions;
using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Logics;

/// <summary>
/// Конвертацяи данных в модели типа <see cref="GeoInformation"/>
/// </summary>
public class Convertor : AbstractErrorHandler, IConvertor
{
    /// <summary>
    /// Полученный набор данных
    /// </summary>
    /// <value></value>
    public GeoInformation? Data {get; private set;}

    /// <summary>
    /// Загрузить данные
    /// </summary>
    /// <param name="fileName"> Полное наименование файла </param>
    /// <returns></returns>
    public Task<bool> Load(string fileName)
    {
        this.Clear();

        try
        {
            var json = File.ReadAllText(fileName);
            Data = JsonSerializer.Deserialize<GeoInformation>(json);    
        }
        catch(Exception ex)
        {
            ErrorText = $"Ошибка при выполнении конвертиации данных в тип {typeof(GeoInformation)}!\n{ex}";
        }
        
        return Task.FromResult(true);
    }


    private record FireRecord(string Region, DateTime Period );

    /// <summary>
    /// Сформировать структуру для выполенния анализа данных по метрикам.
    /// </summary>
    /// <returns></returns> 
    public IDictionary<Region, IList<FireHistory>> CreateFireHistory(DateTime? startPeriod = null, DateTime? stopPeriod = null)
    {
        ArgumentNullException.ThrowIfNull(Data);
        var result = new Dictionary<Region, IList<FireHistory>>();
        if(!Data.features.Any()) return result;

        var items = new HashSet<FireRecord>();
        var regions = new HashSet<string>();
        var data = Data.features 
                    .IfWhere(() => startPeriod is not null, x => x.properties.init_date >= startPeriod)
                    .IfWhere(() => stopPeriod is not null, x => x.properties.init_date <= stopPeriod)
                    .ToList();
        foreach(var feature in data)
        {   
            if(!regions.Contains(feature.properties.name_ru.Trim())) regions.Add(feature.properties.name_ru.Trim());
            var item = new FireRecord(feature.properties.name_ru, feature.properties.init_date);
            if(!items.Contains(item)) items.Add(item);
        }

        var list = regions.Select( x => new { Key =  x, Value = items.Where(y => y.Region.Equals(x, StringComparison.CurrentCultureIgnoreCase))
                    .ToList() }).ToDictionary( x => new Region() { Id =  Guid.NewGuid().ToString(), Name = x.Key}, x => x.Value);
        var histories = list.Select( x => new KeyValuePair<Region, IList<FireHistory>>( x.Key,  
                x.Value.Select(y => new FireHistory()
                {
                    Id =  Guid.NewGuid().ToString(), 
                    Period = y.Period, 
                    Month = y.Period.Month, 
                    Year = y.Period.Year, 
                    RegionId = x.Key.Id 
                }).ToList()));

        result = histories.ToDictionary(x => x.Key, x => x.Value); 
        return result;
    }
}

