using GeoJsonConvertor.Core;
using GeoJsonConvertor.Extensions;
using GeoJsonConvertor.Models;
using Npgsql;

namespace GeoJsonConvertor.Logics;

/// <summary>
/// Реализация хранилища согласно интерфейсу <see cref="IStorage"/>
/// </summary>
public class PgStorage: AbstractErrorHandler, IStorage
{
    private readonly StorageOptions _options;

    public PgStorage(StorageOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // https://www.npgsql.org/doc/basic-usage.html

    /// <summary>
    /// Добавить новый регион
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public async Task Add(Region source)
        => await this.AddInner<Region, FireHistory>(source);
    

    /// <summary>
    /// Добавить новую историю 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public async Task Add(FireHistory source)
        => await this.AddInner<Region, FireHistory>(source);


    /// <summary>
    /// Шаблонный метод для создания SQL запроса на добавление записи
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"> Исходный  тип </typeparam>
    /// <typeparam name="T1"> Один из типов от IModel </typeparam>
    /// <typeparam name="T2"> Один из типов от IModel </typeparam>
    /// <returns></returns>
    private async Task AddInner<T1, T2>(IModel source) 
        where T1: Region
        where T2: FireHistory 
    {
        using var dataSource = new NpgsqlDataSourceBuilder(_options.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();

        var template = "insert into {0}({1}) values({2});";

        var modelRegion = source as T1;
        var modelFireHistory = source as T2;
        var sql = string.Empty;

        IModel model = modelRegion is null ? modelRegion! : modelFireHistory!;
        sql = string.Format(template, 
                    // Наименование таблицы
                    model.Convert<T1>(GeoJsonStorage.ConvertType.TableName),
                    // Список полей
                    model.Convert<T1>(GeoJsonStorage.ConvertType.Head),
                    // Данные
                    model.Convert<T1>(GeoJsonStorage.ConvertType.Data));
        
        
        await using var command = new NpgsqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
    }
}
