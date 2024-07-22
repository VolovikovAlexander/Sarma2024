using System.Data;
using GeoJsonProcessing.Core;
using GeoJsonProcessing.Models;
using System.Text.Json;
using Npgsql;

namespace GeoJsonProcessing.Logics;

/// <summary>
/// Процесс формирования данных по бизнес метрикам согласно интерфейсу <see cref="IProcessing"/>
/// </summary>
public class Processing : IProcessing
{
    // Настройки подключения
    private readonly GeoJsonOptions _options;

    // Набор исходных данных 
    private DataTable _table = new DataTable();
    public Processing(GeoJsonOptions options)  => (_options) = (options);
   
    /// <summary>
    /// Загрузить данные для бизнес метрики
    /// </summary>
    /// <param name="source"> Бизнес метрика </param>
    /// <returns></returns>
    public async Task Build(Metric source)
    {
        ArgumentNullException.ThrowIfNull(source);

        // Получаем описание бизнес метрики
        var metric = await Inrich(source); 
        // Получаем данные по бизнес метрики
        var table = await Select(metric.ConnectionString, metric.Sql);

        if(table.Rows.Count == 0) throw new InvalidOperationException($"Некорректно произведены настройки для бизнес метрики {source}. Нет данных!");

        // Записываем запись в историю
        await Execute(_options.ConnectionString, $"insert into public.work_history(period, source)id) values('{DateTime.Now.ToString("yyyy-MM-dd")}',{metric.SourceId})");
        var history = await Select(_options.ConnectionString, "select id from  public.work_history( order by id limit 1");
        var historyId = int.TryParse(history.Rows[0]["metric_id"].ToString(), out var _id) ? _id : 0;

        if(historyId == 0) throw new InvalidOperationException($"Невозможно получить код записи истории!");

        foreach(var row in table.Rows)
        {
            foreach(var column in metric.Columns)
            {
                // TODO: дописать запрос
                var sql = "insert into public.work_results(metrics_details_id, column, value, work_history_id, row_id)";
            }
        }


        // Запрос: select row_number() over ( order by year ) as row_number, year, count(*) as cnt from public.fire_history where region_id = '210785d9-5886-4961-bd02-1ed709b96887' group by year order by year
    }

    /// <summary>
    /// Дополнить параметры модели
    /// </summary>
    /// <param name="source"> Исходная модель </param>
    /// <returns></returns>
    private async Task<Metric> Inrich(Metric source)
    {
        ArgumentNullException.ThrowIfNull(source);

        // Выборка из базы данных - описание
        var sql =  $"select \"sql\", \"columns\", \"connection_string\", metric_id, source_id from public.metrics_details as t1 inner join public.sources as t2 on t1.source_id = t2.id where t1.id = {source.Id} limit 1";
        var table = await Select(_options.ConnectionString, sql);
        
        var row = table.Rows[0];

        // Загружаем дополнительные параметры
        source.ConnectionString = row["connection_string"].ToString()!;
        source.Sql = row["sql"]?.ToString()!;
        source.Columns = JsonSerializer.Deserialize<List<Column>>(row["columns"].ToString()!)!;
        source.MetricId =  int.TryParse(row["metric_id"].ToString(), out var metricId) ? metricId : 0;
        source.SourceId =   int.TryParse(row["source_id"].ToString(), out var sourceId) ? metricId : 0;

        return source;
    }

    /// <summary>
    /// Выполнить SQL запрос и вернуть результат в виде <see cref="DataTable"/>
    /// </summary>
    /// <param name="connectionString"> Строка соединения </param>
    /// <param name="sql"> SQL запрос </param>
    /// <returns></returns> 
    private async Task<DataTable> Select(string connectionString, string sql)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(sql);

        // Подключаемся
        using var dataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        using var connection = dataSource.OpenConnection();
        await using var command = new NpgsqlCommand(sql, connection);

        // Выполняем запрос 
        using var adapter = new NpgsqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);

        return table;     
    }

    /// <summary>
    /// Выполнить SQL запрос
    /// </summary>
    /// <param name="connectionString"> Строка соединения </param>
    /// <param name="sql"> SQL запрос </param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    private async Task Execute(string connectionString, string sql)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(sql);

        // Подключаемся
        using var dataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        using var connection = dataSource.OpenConnection();
        await using var command = new NpgsqlCommand(sql, connection);

        // Выполняем запрос
        await command.ExecuteNonQueryAsync();
    }


}
