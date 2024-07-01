using System.ComponentModel.DataAnnotations;

namespace GeoJsonConvertor.Extensions;
public static class StringFileNameExtension
{
    /// <summary>
    /// Получить полное наименование файла
    /// </summary>
    /// <param name="fileName"> Текущий файл </param>
    /// <param name="catalog"> Стартовый каталог </param>
    /// <returns></returns>
    public static string ToFileName(this string fileName, string catalog = "")
    {
        var source = fileName.Trim();
        if(File.Exists(source)) return source;
        if(File.Exists(Path.Combine(catalog, source))) return Path.Combine(catalog, source);
        if(string.IsNullOrEmpty(catalog)) return ToFileName(source, Directory.GetCurrentDirectory());
        throw new FileNotFoundException($"Не найден файл {source}, каталог {catalog}!");
    }
}
