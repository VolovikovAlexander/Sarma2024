using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Core;

/// <summary>
/// Интерфейс для выполнения операций обработки geojson файлов
/// </summary>
public interface IConvertor
{
    /// <summary>
    /// Полученная информация после загрузки
    /// </summary> 
    /// <value></value>
    public GeoInformation? Data {get;}

    /// <summary>
    /// Загрузить данные из файла
    /// </summary>
    /// <param name="fileName"> Полное наименование файла </param>
    /// <returns></returns>
    public Task<bool> Load(string fileName);
}
