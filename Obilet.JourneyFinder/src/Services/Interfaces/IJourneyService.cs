using Models.Responses;

namespace Services.Interfaces;

public interface IJourneyService
{
    Task<string> GetBusLocationsSearchAsync(string search);
    
    Task<string> GetAllBusLocationsAsync();
    
    Task<GetJourneysResponse?> GetJourneysAsync(int originId, int destinationId, DateTime departureDate);
}