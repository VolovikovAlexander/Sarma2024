namespace GeoJsonStorage;

/// <summary>
/// Типы хранилища данных
/// </summary> 
public enum StorageType
{
    PostgreSQL = 1
}

/// <summary>
/// Тип конвертации данных
/// </summary>
public enum ConvertType
{
    /// <summary>
    /// Наименование полей
    /// </summary>
    Head = 1,

    /// <summary>
    /// Набор значений
    /// </summary>
    Data = 2,

    /// <summary>
    /// Наименование таблицы
    /// </summary>
    TableName = 3
}