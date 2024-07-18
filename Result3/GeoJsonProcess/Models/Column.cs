
using System.Text.Json.Serialization;

namespace GeoJsonProcessing.Models;

/// <summary>
/// Описание колонки
/// </summary>
public class Column
{
    /// <summary>
    /// Наименование колонки
    /// </summary>
    /// <value></value>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тип данных
    /// </summary>
    /// <value></value>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

