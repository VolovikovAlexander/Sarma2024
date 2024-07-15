
namespace GeoJsonConvertor.Extensions;
public static class StringFileNameExtension
{
    /// <summary>
    /// Получить полное наименование файла
    /// </summary>
    /// <param name="fileName"> Текущий файл </param>
    /// <param name="catalog"> Стартовый каталог </param>
    /// <returns></returns>
    public static string ToFullFileName(this string fileName, string catalog = "")
    {
        var source = fileName.Trim();
        if(File.Exists(source)) return source;
        if(File.Exists(Path.Combine(catalog, source))) return Path.Combine(catalog, source);

        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? Directory.GetCurrentDirectory();
        if(string.IsNullOrEmpty(catalog)) return ToFullFileName(source, path);
        throw new FileNotFoundException($"Не найден файл {source}, каталог {catalog}!");
    }
}
