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

        var convertor =  new Convertor();
        var result = await convertor.Load(fileName);
        if(result) Console.WriteLine($"Данные из файла {fileName} загружены успешно!");

        Console.ReadLine();
    }
}
