namespace GeoJsonConvertor.Models;
public class FireHistory
{
    public string Id { get; set;} = Guid.NewGuid().ToString();

    public string RegionId { get; set;} = Guid.NewGuid().ToString();

    public int Year {get; set;}     

    public int Month {get; set;}

    public DateTime Period {get; set;}  = DateTime.Now;
}
