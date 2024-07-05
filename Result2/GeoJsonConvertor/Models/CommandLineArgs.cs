
namespace GeoJsonConvertor.Models;

/// <summary>
/// Модель данных свойства которой будут передаваться через командную строку
/// </summary> 
public class CommandLineArgs
{
    /// <summary>
    /// Наименование файла
    /// </summary> 
    [PositionArgs(1)]
    public string FileName { get; set; }  = string.Empty;

    /// <summary>
    /// Дата начала периода фильтрации
    /// </summary>
    /// <value></value>
    [PositionArgs(2)]
    public DateTime StartPeriod {get; set; } = DateTime.Now;

    /// <summary>
    /// Дата окончания прерида фильтрации
    /// </summary>
    /// <value></value>
    [PositionArgs(3)]
    public DateTime StopPeriod {get; set; } = DateTime.Now;
}


/// <summary>
/// Класс атрибут. Используется для фиксирования позиции свойства в аргументах командной строки
/// </summary> 
internal class PositionArgsAttribute : Attribute
{
    public PositionArgsAttribute(int position)
    {
        Position = position;
    }

    /// <summary>
    /// Номер позиции
    /// </summary>
    /// <value></value>    
    public int Position { get; private set; } = 1;
}