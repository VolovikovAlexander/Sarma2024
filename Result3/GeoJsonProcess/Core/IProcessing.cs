﻿namespace GeoJsonConvertor.Core;

/// <summary>
/// Интерфейс к процессу формирования бизнес метрик
/// </summary>
public interface IProcessing
{
    /// <summary>
    /// Сформировать набор бизнес метрик
    /// </summary>
    /// <returns></returns> 
    public Task Build();
}
