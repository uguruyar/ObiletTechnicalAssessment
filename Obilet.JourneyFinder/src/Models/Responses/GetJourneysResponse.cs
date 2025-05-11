using System.Text.Json.Serialization;

namespace Models.Responses;

public class GetJourneysResponse
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("data")]
    public List<JourneyItem>? Data { get; set; }
}

public class JourneyItem
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("partner-name")]
    public string? PartnerName { get; set; }

    [JsonPropertyName("bus-type-name")]
    public string? BusTypeName { get; set; }

    [JsonPropertyName("total-seats")]
    public int TotalSeats { get; set; }

    [JsonPropertyName("available-seats")]
    public int AvailableSeats { get; set; }

    [JsonPropertyName("journey")]
    public JourneyDetail? Journey { get; set; }
}

public class JourneyDetail
{
    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    [JsonPropertyName("destination")]
    public string? Destination { get; set; }

    [JsonPropertyName("departure")]
    public DateTime Departure { get; set; }

    [JsonPropertyName("arrival")]
    public DateTime Arrival { get; set; }

    [JsonPropertyName("price")]
    public decimal InternetPrice { get; set; }

    [JsonPropertyName("bus-name")]
    public string? BusName { get; set; }
}