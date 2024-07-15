using GeoJsonStorage;

namespace GeoJsonConvertor.Core;

/// <summary>
/// Настройки для работы с хранилищем данных
/// </summary>
public class GeoJsonOptions
{
    /// <summary>
    /// Строка соединения с базой данных
    /// </summary>
    /// <value></value>
    public string ConnectionString { get; set; }   = string.Empty;

    /// <summary>
    /// Тип хранилища данных
    /// </summary> 
    public StorageType StorageType { get; set; } = StorageType.PostgreSQL;
}
