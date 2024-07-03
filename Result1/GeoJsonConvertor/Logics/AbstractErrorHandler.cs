using GeoJsonConvertor.Core;

namespace GeoJsonConvertor.Logics;

/// <summary>
/// Абстрактный класс для работы с обработкой ошибок
/// </summary>
public class AbstractErrorHandler : IErrorHandler
{
    /// <summary>
    /// Флаг. Наличие ошибки
    /// </summary> <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsError => !string.IsNullOrWhiteSpace(ErrorText);

    /// <summary>
    /// Описание ошибки
    /// </summary>
    /// <value></value>
    public string ErrorText { get ; set; } = string.Empty;

    /// <summary>
    /// Очистить информацию об ошибке
    /// </summary> 
    public virtual void Clear()
    {
        ErrorText = string.Empty;
    }
}
