using System.Text.Json.Serialization;

namespace Models.Responses;

public class GetBusLocationsResponse
{
    [JsonPropertyName("status")] public string? Status { get; set; }

    [JsonPropertyName("data")] public List<BusLocation>? Data { get; set; }

    [JsonPropertyName("message")] public string? Message { get; set; }

    [JsonPropertyName("user-message")] public string? UserMessage { get; set; }

    [JsonPropertyName("api-request-id")] public string? ApiRequestId { get; set; }

    [JsonPropertyName("controller")] public string? Controller { get; set; }

    [JsonPropertyName("client-request-id")]
    public string? ClientRequestId { get; set; }

    [JsonPropertyName("web-correlation-id")]
    public string? WebCorrelationId { get; set; }

    [JsonPropertyName("correlation-id")] public string? CorrelationId { get; set; }

    [JsonPropertyName("parameters")] public object? Parameters { get; set; }
}

public class GeoLocation
{
    [JsonPropertyName("latitude")] public double? Latitude { get; set; }

    [JsonPropertyName("longitude")] public double? Longitude { get; set; }

    [JsonPropertyName("zoom")] public int Zoom { get; set; }
}

public class BusLocation
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("parent-id")] public int ParentId { get; set; }

    [JsonPropertyName("type")] public string? Type { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("geo-location")] public GeoLocation? GeoLocation { get; set; }

    [JsonPropertyName("zoom")] public int Zoom { get; set; }

    [JsonPropertyName("tz-code")] public string? TimeZoneCode { get; set; }

    [JsonPropertyName("weather-code")] public string? WeatherCode { get; set; }

    [JsonPropertyName("rank")] public int Rank { get; set; }

    [JsonPropertyName("reference-code")] public string? ReferenceCode { get; set; }

    [JsonPropertyName("city-id")] public int CityId { get; set; }

    [JsonPropertyName("reference-country")]
    public string? ReferenceCountry { get; set; }

    [JsonPropertyName("country-id")] public int CountryId { get; set; }

    [JsonPropertyName("keywords")] public string? Keywords { get; set; }

    [JsonPropertyName("city-name")] public string? CityName { get; set; }

    [JsonPropertyName("languages")] public object? Languages { get; set; }

    [JsonPropertyName("country-name")] public string? CountryName { get; set; }

    [JsonPropertyName("code")] public string? Code { get; set; }

    [JsonPropertyName("show-country")] public bool ShowCountry { get; set; }

    [JsonPropertyName("area-code")] public string? AreaCode { get; set; }

    [JsonPropertyName("long-name")] public string? LongName { get; set; }

    [JsonPropertyName("is-city-center")] public bool IsCityCenter { get; set; }
}