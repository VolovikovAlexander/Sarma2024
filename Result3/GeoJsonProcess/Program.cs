﻿using GeoJsonConvertor.Core;
using GeoJsonConvertor.Logics;
using Microsoft.Extensions.Configuration;

namespace GeoJsonConvertor;

class Program
{
    static async Task Main(string[] args)
    {
        ConsoleWrite("Приложение запущено.");

        // Подключаем настройки
        var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

        var databaseOptions = config.GetSection("DatabaseOptions").Get<GeoJsonOptions>() ?? throw new InvalidOperationException("Невозможно получить конфигурационные данные из файла appsettings.json!");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(databaseOptions?.ConnectionString);    

        var processingOptions = config.GetSection("ProcessingOptions").Get<ProcessingOptions>() ?? throw new InvalidOperationException("Невозможно получить конфигурационные данные из файла appsettings.json!");
        ArgumentNullException.ThrowIfNull(processingOptions);

        // Запускаем процесс формирования бизнес местрик


        ConsoleWrite("Приложение завершено.");
        Console.ReadLine();
    }

    static void ConsoleWrite(string message) => Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} {message}");
}
