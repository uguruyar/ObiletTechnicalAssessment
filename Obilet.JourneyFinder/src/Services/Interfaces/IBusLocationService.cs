using Models.Responses;

namespace Services.Interfaces;

public interface IBusLocationService
{
    Task<List<BusLocation>> GetBusLocationAsync(string origin = null, CancellationToken cancellationToken = default);
}