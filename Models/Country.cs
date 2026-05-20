namespace WorldApi.Models;

public class Country
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Continent { get; set; } = "Asia";
    public string Region { get; set; } = string.Empty;
    public decimal SurfaceArea { get; set; }
    public short? IndepYear { get; set; }
    public int Population { get; set; }
    public decimal? LifeExpectancy { get; set; }
    public decimal? GNP { get; set; }
    public decimal? GNPOld { get; set; }
    public string LocalName { get; set; } = string.Empty;
    public string GovernmentForm { get; set; } = string.Empty;
    public string? HeadOfState { get; set; }
    public int? Capital { get; set; }
    public string Code2 { get; set; } = string.Empty;
}
