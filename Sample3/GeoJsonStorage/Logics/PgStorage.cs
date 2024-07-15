using GeoJsonConvertor.Core;
using GeoJsonConvertor.Extensions;
using GeoJsonConvertor.Models;
using Npgsql;

namespace GeoJsonConvertor.Logics;

/// <summary>
/// Реализация хранилища согласно интерфейсу <see cref="IStorage"/>
/// </summary>
public class PgStorage : AbstractErrorHandler, IStorage
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
    /// Сохранить данные
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public async Task Save(IDictionary<Region, IList<FireHistory>> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        Clear();

        // Загрузим новый список
        foreach (var region in source.Keys)
        {
            // Добавляем новые регионы
            await Add(region);
            foreach (var row in source[region])
            {
                // Добавляем новые записи истории
                await Add(row);
            }
        }
    }


    /// <summary>
    /// Очистить базу данных
    /// </summary>
    public override void Clear()
    {
        base.Clear();

        var dataSource = new NpgsqlDataSourceBuilder(_options.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();
        using var command = new NpgsqlCommand("delete from regions;", connection);

        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Шаблонный метод для создания SQL запроса на добавление записи
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"> Исходный  тип </typeparam>
    /// <typeparam name="T1"> Один из типов от IModel </typeparam>
    /// <typeparam name="T2"> Один из типов от IModel </typeparam>
    /// <returns></returns>
    private async Task AddInner<T1, T2>(IModel source)
        where T1 : Region
        where T2 : FireHistory
    {
        using var dataSource = new NpgsqlDataSourceBuilder(_options.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();

        var template = "insert into {0}({1}) values({2});";

        var modelRegion = source as T1;
        var modelFireHistory = source as T2;
        var sql = string.Empty;

        IModel model = modelRegion is not null ? modelRegion! : modelFireHistory!;
        sql = string.Format(template,
                    // Наименование таблицы
                    model.Convert(GeoJsonStorage.ConvertType.TableName),
                    // Список полей
                    model.Convert(GeoJsonStorage.ConvertType.Head),
                    // Данные
                    model.Convert(GeoJsonStorage.ConvertType.Data));


        await using var command = new NpgsqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
    }
}
