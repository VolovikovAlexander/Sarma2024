using GeoJsonConvertor.Core;
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
        var options = config.GetSection("ProcessOptions").Get<GeoJsonOptions>() ?? throw new InvalidOperationException("Невозможно получить конфигурационные данные из файла appsettings.json!");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(options?.ConnectionString);    

      

        ConsoleWrite("Приложение завершено.");
        Console.ReadLine();
    }

    static void ConsoleWrite(string message) => Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} {message}");
}
