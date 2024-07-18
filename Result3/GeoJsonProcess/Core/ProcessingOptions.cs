using GeoJsonProcessing.Models;

namespace GeoJsonProcessing.Core;


/// <summary>
/// Настройки процесса формирования бизнес метрик
/// </summary>
public class ProcessingOptions
{
    /// <summary>
    /// Список бизнес метрик которые будем формировать
    /// </summary>
    /// <value></value>
    public required IList<Metric> BuildMetrics {get; set;}
}
