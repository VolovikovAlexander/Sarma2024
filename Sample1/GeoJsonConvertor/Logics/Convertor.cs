using GeoJsonConvertor.Core;
using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Logics;
public class Convertor : IConvertor
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
        throw new NotImplementedException();
    }
}
