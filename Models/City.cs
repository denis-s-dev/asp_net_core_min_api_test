namespace WorldApi.Models;

public class City
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public int Population { get; set; }
}
