
namespace GeoJsonConvertor.Models;

/// <summary>
/// Модель данных свойства которой будут передаваться через командную строку
/// </summary> 
public class CommandLineArgs
{
    /// <summary>
    /// Наименование файла
    /// </summary> 
    [PositionArgs(1, typeof(string))]
    public string FileName { get; set; }  = string.Empty;

    /// <summary>
    /// Дата начала периода фильтрации
    /// </summary>
    /// <value></value>
    [PositionArgs(2, typeof(DateTime))]
    public DateTime StartPeriod {get; set; } = new DateTime(1900,1,1);

    /// <summary>
    /// Дата окончания прерида фильтрации
    /// </summary>
    /// <value></value>
    [PositionArgs(3, typeof(DateTime))]
    public DateTime StopPeriod {get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"Переданные параметры: {FileName},{StartPeriod.ToString()}, {StopPeriod.ToString()}";
    }
}


/// <summary>
/// Класс атрибут. Используется для фиксирования позиции свойства в аргументах командной строки
/// </summary> 
internal class PositionArgsAttribute : Attribute
{
    public PositionArgsAttribute(int position, Type type)
    {
        Position = position;
        Type = type;
    }

    /// <summary>
    /// Номер позиции
    /// </summary>
    /// <value></value>    
    public int Position { get; private set; } = 1;

    /// <summary>
    /// Тип для конвертации
    /// </summary>
    /// <returns></returns>
    public Type Type {get;private set; } = typeof(string);
}