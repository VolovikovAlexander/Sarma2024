namespace GeoJsonConvertor.Core;
public interface IErrorHandler
{
    /// <summary>
    /// Флаг. Есть ошибка
    /// </summary> 
    public bool IsError { get; }
    
    /// <summary>
    /// Описание ошибки
    /// </summary>
    /// <value></value>
    public string ErrorText {get; set;}

    /// <summary>
    /// Очистить флаг об ошибке
    /// </summary>
    public void Clear();
}
