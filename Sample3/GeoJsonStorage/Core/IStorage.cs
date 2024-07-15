
using System.Security.Cryptography.X509Certificates;
using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Core;

/// <summary>
/// Интерфейс к хранилищу данных
/// </summary>
public interface IStorage: IErrorHandler
{
    /// <summary>
    /// Добавить новый регион
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public Task Add(Region source);

    /// <summary>
    /// Добавить новую запись об истории пожара
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public Task Add(FireHistory source);

    /// <summary>
    /// Сохранить данные по истории
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public Task Save(IDictionary<Region, IList<FireHistory>> source);
}
