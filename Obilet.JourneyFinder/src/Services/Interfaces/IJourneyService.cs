using Models;
using Models.Responses;

namespace Services.Interfaces;

public interface IJourneyService
{
    Task<GetBusLocationsResponse> GetBusLocationsAsync(string? search = null);

    Task<List<JourneySummary>?> GetJourneysAsync(int originId, int destinationId, DateTime departureDate);
}