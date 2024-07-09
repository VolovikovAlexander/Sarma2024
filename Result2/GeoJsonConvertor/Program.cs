using GeoJsonConvertor.Extensions;
using GeoJsonConvertor.Logics;

namespace GeoJsonConvertor;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} Приложение запущено.");
        var properties = args.GetProperties();
        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} {properties}");

        var fileName =  properties.FileName.ToFullFileName();
        if(!File.Exists(fileName)) throw new FileNotFoundException($"Не найден указанный файл {fileName}!");

        var convertor =  new Convertor();
        var result = await convertor.Load(fileName);
        if(result)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} Данные из файла {fileName} загружены успешно.");
            var history = convertor.CreateFireHistory(properties.StartPeriod, properties.StopPeriod);
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} История для записи подготовлена.");
            Console.WriteLine(history.CreateAnalyzeTable());
        }

        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} Приложение завершено.");
    }
}
