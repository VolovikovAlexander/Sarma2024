using System.Data;
using GeoJsonProcessing.Core;
using GeoJsonProcessing.Models;
using System.Text.Json;
using Npgsql;
using System.Text.Json.Serialization;

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
    public async Task Build(Metric source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        source = await Refresh(source);
        await LoadData(source);

        // Подключаемся к базе данных назначение
        using var dataSource = new NpgsqlDataSourceBuilder(_options.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();

        // Добавим запись в историю
        var sql = $"insert into public.metric_history(period, source_id) values ('{DateTime.Now.ToString("yyyy-dd-MM")}',{source.MetricId})";
        var command = new NpgsqlCommand(source.Sql, connection);
        await command.ExecuteNonQueryAsync();

        foreach(var row in _table.Rows)
        {
            foreach(var column in _table.Columns)
            {
                var columnName = (column as DataColumn)?.ColumnName ?? throw new InvalidOperationException("Невозможно получить информацию!");
                var columnData = (row as DataRowView)?[columnName].ToString() ?? "null";
                sql = $"insert into public.metrics_values(metric_details_id, \"column\", value ) values({columnName}','{columnData});";
                command = new NpgsqlCommand(sql, connection);
                await command.ExecuteScalarAsync();
            }
        }
    }

    /// <summary>
    /// Загрузить данные для бизнес метрики
    /// </summary>
    /// <param name="source"> Бизнес метрика </param>
    /// <returns></returns>
    private async Task LoadData(Metric source)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentException.ThrowIfNullOrWhiteSpace(source.ConnectionString, "Параметр ConnectionString не указан!");

        // Подключаемся к исходной базе данных 
        // https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter
        using var dataSource = new NpgsqlDataSourceBuilder(source.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();
        await using var command = new NpgsqlCommand(source.Sql, connection);

        // Выполняем запрос 
        using var adapter = new NpgsqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
    }

    /// <summary>
    /// Загрузить дополнительные параметры
    /// </summary>
    /// <param name="source"> Исходная модель </param>
    /// <returns></returns>
    private async Task<Metric> Refresh(Metric source)
    {
        ArgumentNullException.ThrowIfNull(source);

        using var dataSource = new NpgsqlDataSourceBuilder(_options.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();
        var sql =  $"select \"sql\", \"columns\", \"connection_string\", metric_id from public.metrics_details as t1 inner join public.sources as t2 on t1.source_id = t2.id where t1.id = {source.Id} limit 1";
        await using var command = new NpgsqlCommand(sql, connection);

        // Выполняем запрос 
        using var adapter = new NpgsqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);

        if(table.Rows.Count == 0) throw new InvalidDataException($"Неккорректно указаны настройки. Нет данных для кода {source.MetricId}!");

        var row = table.Rows[0];
        source.ConnectionString = row["connection_string"].ToString()!;
        source.Sql = row["sql"]?.ToString()!;
        source.Columns = JsonSerializer.Deserialize<List<Column>>(row["columns"].ToString()!)!;
        source.MetricId =  int.TryParse(row["metric_id"].ToString(), out var metricId) ? metricId : 0;

        return source;
    }
}
