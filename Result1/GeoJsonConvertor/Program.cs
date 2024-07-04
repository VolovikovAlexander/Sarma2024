using GeoJsonConvertor.Extensions;
using GeoJsonConvertor.Logics;

namespace GeoJsonConvertor;

class Program
{
    static async Task Main(string[] args)
    {
        if(args.Length < 1) throw new ArgumentException("Необходимо передать аргументы! Наименование файла.");

        var fileName =  args[0].ToFullFileName();
        if(!File.Exists(fileName)) throw new FileNotFoundException($"Не найден указанный файл {fileName}!");

        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} приложение запущено.");
        var convertor =  new Convertor();
        var result = await convertor.Load(fileName);
        if(result)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} Данные из файла {fileName} загружены успешно!");
            var history = convertor.CreateFireHistory();
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")} История для записи подготовлена.");
        }
    }
}
