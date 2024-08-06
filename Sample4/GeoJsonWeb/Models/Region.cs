using GeoJsonConvertor.Core;

namespace GeoJsonConvertor.Models;
public class Region: IModel
{
    /// <summary>
    /// Уникальный код региона
    /// </summary>
    /// <returns></returns>
    public string Id {get;set;} = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Наименование
    /// </summary>
    /// <value></value>
    public string Name {get;set;} = string.Empty;
}
