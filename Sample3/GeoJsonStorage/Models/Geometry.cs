namespace GeoJsonConvertor.Models;
public class Geometry
{
    public string type { get; set; } = string.Empty;
    public List<List<List<List<double>>>> coordinates { get; set; } = new();
}



