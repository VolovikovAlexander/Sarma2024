using System.Data;
using GeoJsonConvertor.Core;
using GeoJsonIProcessing.Models;
using Npgsql;

namespace GeoJsonIProcessing.Logics;

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
        // Подключаемся к исходной базе данных 
        using var dataSource = new NpgsqlDataSourceBuilder(source.ConnectionString).Build();
        using var connection = dataSource.OpenConnection();
        await using var command = new NpgsqlCommand(source.Sql, connection);

        // Выполняем запрос 
        using var adapter = new NpgsqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
    }
}
