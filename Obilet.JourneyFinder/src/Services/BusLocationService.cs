using Models.Base;
using Models.Responses;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services;

public class BusLocationService : IBusLocationService
{
    private readonly IJourneyService _journeyService;
    private readonly IMemoryCacheProvider _memoryCacheProvider;

    public BusLocationService(IJourneyService journeyService, IMemoryCacheProvider memoryCacheProvider)
    {
        _journeyService = journeyService;
        _memoryCacheProvider = memoryCacheProvider;
    }

    public async Task<List<BusLocation>> GetBusLocationAsync(string origin = null,
        CancellationToken cancellationToken = default)
    {
        var journeys = _memoryCacheProvider.Get<List<BusLocation>>(Constants.BusLocationCacheKey);

        if (journeys == null)
        {
            var response = await _journeyService.GetBusLocationsAsync();

            journeys = response.Data;
            _memoryCacheProvider.Save(Constants.BusLocationCacheKey, journeys);
        }

        return string.IsNullOrWhiteSpace(origin)
            ? journeys
            : journeys.Where(x => x.Name!.Contains(origin) || x.Keywords!.Contains(origin)).ToList();
    }
}