namespace GeoJsonConvertor.Models;
public class Feature
{
    public string type { get; set; } = string.Empty;
    public Properties properties { get; set; } = new();
    public Geometry geometry { get; set; } = new();
}