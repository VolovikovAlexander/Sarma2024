namespace GeoJsonConvertor.Models;
public class GeoInformation
{
    public string type { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public Crs crs { get; set; } = new();
    public List<Feature> features { get; set; } = new();
}
