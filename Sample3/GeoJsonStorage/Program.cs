using GeoJsonConvertor.Core;
using GeoJsonConvertor.Logics;
using Microsoft.Extensions.Configuration;

namespace GeoJsonConvertor;

class Program
{
    static async Task Main(string[] args)
    {
        ConsoleWrite("Приложение запущено.");
        var fileName = @"template.geojson";
        if(!File.Exists(fileName)) throw new FileNotFoundException($"Не найден указанный файл {fileName}!");

        var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
        var options = config.GetSection("StorageOptions").Get<StorageOptions>() ?? throw new InvalidOperationException("Невозможно получить конфигурационные данные из файла appsettings.json!");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(options?.ConnectionString);      

        var convertor =  new Convertor();
        var result = await convertor.Load(fileName);
        if(result)
        {
            ConsoleWrite($"Данные из файла {fileName} загружены успешно.");
            var history = convertor.CreateFireHistory();
        }

        ConsoleWrite("Приложение завершено.");
        Console.ReadLine();
    }

    static void ConsoleWrite(string message) => Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} {message}");
}
