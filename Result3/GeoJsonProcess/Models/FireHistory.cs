using GeoJsonConvertor.Core;

namespace GeoJsonConvertor.Models;
public class FireHistory: IModel
{
    /// <summary>
    /// Уникальный код записи
    /// </summary>
    public string Id { get; set;} = Guid.NewGuid().ToString();

    /// <summary>
    /// Уникальный код региона
    /// </summary>
    /// <returns></returns>
    public string RegionId { get; set;} = Guid.NewGuid().ToString();

    /// <summary>
    /// Год фиксации пожара
    /// </summary>
    /// <value></value>
    public int Year {get; set;}     

    /// <summary>
    /// Месяц фиксации пожара
    /// </summary>
    /// <value></value>
    public int Month {get; set;}

    /// <summary>
    /// Дата фиксации пожара
    /// </summary>
    public DateTime Period {get; set;}  = DateTime.Now;
}
