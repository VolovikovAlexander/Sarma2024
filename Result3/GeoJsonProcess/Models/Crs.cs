namespace GeoJsonConvertor.Models;
public class Crs
{
    public string type { get; set; } = string.Empty;
    public Properties properties { get; set; } = new();
}
