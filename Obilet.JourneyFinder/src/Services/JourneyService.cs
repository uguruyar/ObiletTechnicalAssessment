using System.Text.Json;
using Models;
using Models.Requests;
using Models.Responses;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services;

public class JourneyService : IJourneyService
{
    private readonly IMemoryCacheProvider _memoryCacheProvider;
    private readonly IObiletApiClient _apiClient;

    public JourneyService(IMemoryCacheProvider repo, IObiletApiClient apiClient)
    {
        _memoryCacheProvider = repo;
        _apiClient = apiClient;
    }
    
    public async Task<string> GetBusLocationsSearchAsync(string search)
    {
        var session = await GetSessionAsync();

        var body = new GetBusLocationsRequest
        {
            Data = search,
            Language = "tr-TR",
            Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            DeviceSession = new DeviceSession
            {
                SessionId = session.SessionId,
                DeviceId = session.DeviceId
            }
        };

        return await _apiClient.CallObiletEndpoint("api/location/getbuslocations", body);
    }

    public async Task<string> GetAllBusLocationsAsync()
    {
        var session = await GetSessionAsync();

        var body = new GetBusLocationsRequest
        {
            Data = null,
            Language = "tr-TR",
            Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            DeviceSession = new DeviceSession
            {
                SessionId = session.SessionId,
                DeviceId = session.DeviceId
            }
        };

        return await _apiClient.CallObiletEndpoint("api/location/getbuslocations", body);
    }
    
    public async Task<GetJourneysResponse?> GetJourneysAsync(int originId, int destinationId, DateTime departureDate)
    {
        var session = await GetSessionAsync();

        var request = new GetJourneysRequest
        {
            Date = departureDate.ToString("yyyy-MM-dd"),
            Language = "tr-TR",
            DeviceSession = new DeviceSession
            {
                SessionId = session.SessionId,
                DeviceId = session.DeviceId
            },
            Data = new JourneyData
            {
                OriginId = originId,
                DestinationId = destinationId,
                DepartureDate = departureDate.ToString("yyyy-MM-dd")
            }
        };

        var jsonResponse = await _apiClient.CallObiletEndpoint("api/journey/getbusjourneys", request);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var journeysResponse = JsonSerializer.Deserialize<GetJourneysResponse>(jsonResponse, options);

        return journeysResponse;
    }
    
    private async Task<SessionData> GetSessionAsync()
    {
        var session = _memoryCacheProvider.Get();
        if (session == null)
        {
            session = await _apiClient.GetSessionAsync();
            _memoryCacheProvider.Save(session);
        }

        return session;
    }
}