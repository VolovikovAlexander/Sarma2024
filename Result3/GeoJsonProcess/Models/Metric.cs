﻿namespace GeoJsonIProcessing.Models;

/// <summary>
/// Модель описание бизнес метрик
/// </summary>
public class Metric
{
    /// <summary>
    /// Уникальный код
    /// </summary>
    /// <value></value>
    public long Id {get; set;}

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name {get; set;} = string.Empty;

    /// <summary>
    /// Строка соединения с базой данных
    /// </summary>
    /// <value></value>
    public string ConnectionString {get; set;} = string.Empty;

    /// <summary>
    /// SQL запрос для выборки данных
    /// </summary>
    /// <value></value>
    public string Sql {get; set;} = string.Empty;

    /// <summary>
    /// Список колонок SQL запроса
    /// </summary>
    public IList<Column> Columns {get; set;} = new List<Column>();
}