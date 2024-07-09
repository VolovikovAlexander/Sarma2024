using GeoJsonConvertor.Extensions;
using GeoJsonConvertor.Logics;

namespace GeoJsonConvertor;

class Program
{
    static async Task Main(string[] args)
    {
        ConsoleWrite("Приложение запущено.");
        var properties = args.GetProperties();
        ConsoleWrite($"{properties}");

        var fileName =  properties.FileName.ToFullFileName();
        if(!File.Exists(fileName)) throw new FileNotFoundException($"Не найден указанный файл {fileName}!");

        var convertor =  new Convertor();
        var result = await convertor.Load(fileName);
        if(result)
        {
            ConsoleWrite($"Данные из файла {fileName} загружены успешно.");
            ConsoleWrite($"Дата начала {properties.StartPeriod}, дата окончания {properties.StopPeriod}");
            var history = convertor.CreateFireHistory(properties.StartPeriod, properties.StopPeriod);
            ConsoleWrite("История для записи подготовлена.");
            Console.WriteLine(history.CreateAnalyzeTable());
        }

        ConsoleWrite("Приложение завершено.");
        Console.ReadLine();
    }

    static void ConsoleWrite(string message) => Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} {message}");
}
