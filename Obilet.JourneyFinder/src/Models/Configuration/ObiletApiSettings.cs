namespace Models.Configuration;

public class ObiletApiSettings
{
    public string BaseUrl { get; set; } = "https://v2-api.obilet.com/";
    public string ApiToken { get; set; } = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
    public string Language { get; set; } = "tr-TR";
    public int SessionExpirationDays { get; set; } = 1;
} 