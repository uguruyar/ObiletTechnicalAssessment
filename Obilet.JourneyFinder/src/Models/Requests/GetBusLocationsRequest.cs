using System.Text.Json.Serialization;

namespace Models.Requests;

public class GetBusLocationsRequest
{
    [JsonPropertyName("data")] public string? Data { get; set; } = null;

    [JsonPropertyName("language")] public string Language { get; set; } = "tr-TR";

    [JsonPropertyName("date")] public string? Date { get; set; }

    [JsonPropertyName("device-session")] public DeviceSession? DeviceSession { get; set; }
}

public class DeviceSession
{
    [JsonPropertyName("session-id")] public string? SessionId { get; set; }

    [JsonPropertyName("device-id")] public string? DeviceId { get; set; }
}