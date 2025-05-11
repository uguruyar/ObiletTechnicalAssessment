using System.Text.Json.Serialization;

namespace Models.Requests;

public class GetJourneysRequest
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = "tr-TR";

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("data")]
    public JourneyData? Data { get; set; }

    [JsonPropertyName("device-session")]
    public DeviceSession? DeviceSession { get; set; }
}

public class JourneyData
{
    [JsonPropertyName("origin-id")]
    public int OriginId { get; set; }

    [JsonPropertyName("destination-id")]
    public int DestinationId { get; set; }

    [JsonPropertyName("departure-date")]
    public string? DepartureDate { get; set; }
}