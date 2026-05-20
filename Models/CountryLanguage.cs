namespace WorldApi.Models;

public class CountryLanguage
{
    public string CountryCode { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string IsOfficial { get; set; } = "F";
    public decimal Percentage { get; set; }
}
