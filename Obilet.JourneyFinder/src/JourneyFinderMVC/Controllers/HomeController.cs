using System.Diagnostics;
using JourneyFinderMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace JourneyFinderMVC.Controllers;

public class HomeController : Controller
{
    private readonly IBusLocationService _busLocationService;

    public HomeController(IBusLocationService busLocationService)
    {
        _busLocationService = busLocationService;
    }

    public async Task<IActionResult> Index()
    {
        var journeys = await _busLocationService.GetBusLocationAsync();

        TempData["Journeys"] = journeys;

        var model = new JourneySearchModel
        {
            Origin = journeys[0].Name,
            OriginId = journeys[0].Id,
            Destination = journeys[1].Name,
            DestinationId = journeys[1].Id,
            DepartureDate = DateTime.Today
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}