using Models;
using Models.Base;
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

    public async Task<GetBusLocationsResponse> GetBusLocationsAsync(string? search = null)
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

        var response = await _apiClient.CallObiletEndpoint<GetBusLocationsResponse>("api/location/getbuslocations", body);

        if (response.Status != "Success")
        {
            throw new Exception(response.UserMessage);
        }
        return response;
    }

    public async Task<List<JourneySummary>?> GetJourneysAsync(int originId, int destinationId, DateTime departureDate)
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

        var response =  await _apiClient.CallObiletEndpoint<GetJourneysResponse>("api/journey/getbusjourneys", request);

        if (response.Status != "Success")
        {
            throw new Exception(response.UserMessage);
        }
        
        if (response?.Data == null)
            return new List<JourneySummary>();
        
        var journeySummaries = response.Data.Select(j => new JourneySummary
        {
            Origin = j.OriginLocation,
            Destination = j.DestinationLocation,
            Departure = j.Journey.Departure,
            Arrival = j.Journey.Arrival,
            OriginalPrice = j.Journey.OriginalPrice,
            Currency = j.Journey.Currency
        }).ToList();

        return journeySummaries;
    }

    private async Task<SessionData> GetSessionAsync()
    {
        var session = _memoryCacheProvider.Get<SessionData>(Constants.ObiletSessionKey);
        if (session == null)
        {
            session = await _apiClient.GetSessionAsync();
            _memoryCacheProvider.Save(Constants.ObiletSessionKey,session);
        }

        return session;
    }
}