using System.Text;
using GeoJsonConvertor.Models;

namespace GeoJsonConvertor.Extensions;

public static class FireHistoryExtension
{
    /// <summary>
    /// Сформировать аналитическую таблицу с количеством пожаров
    /// </summary>
    /// <param name="source"> Исходная структура типа <see cref="IDictionary<Region, IList<FireHistory>>"/></param>
    /// <returns></returns> 
    public static string CreateAnalyzeTable(this IDictionary<Region, IList<FireHistory>>  source)
    {
        var buider = new StringBuilder();
        buider.AppendLine("|   Регион           |       Кол-во пожаров     |");
        buider.AppendLine("|--------------------|--------------------------|");

        foreach(var row in source.Keys.OrderBy(x => x.Name))
        {
            buider.AppendLine($"| {row.Name}   | {source[row].Count()} |");
        }

        buider.AppendLine("|--------------------|--------------------------|");
        return buider.ToString();
    }
}
