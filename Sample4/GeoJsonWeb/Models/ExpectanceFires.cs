using GeoJsonConvertor.Core;

namespace GeoJsonWeb;

/// <summary>
/// Модель ожиданий по пожарам
/// </summary>
public class ExpectanceFires: IModel
{
    /// <summary>
    /// Уникальный номер 202402б 202403 и т.д.
    /// </summary>
    /// <value></value>
    public string Id { get; set;}

    /// <summary>
    /// Количество пожаров, которые мы лжидаем
    /// </summary>
    /// <value></value>
    public long Count {get;set;}
}
