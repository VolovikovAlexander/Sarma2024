using System.Text.Json;
using GeoJsonConvertor.Core;
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
}
