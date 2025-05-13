using JourneyFinderMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace JourneyFinderMVC.Controllers;

public class JourneyController : Controller
{
    private readonly IJourneyService _journeyService;
    
    public JourneyController(IJourneyService journeyService)
    {
        _journeyService = journeyService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(JourneySearchModel journeySearchModel)
    {
        var journeys = await _journeyService.GetJourneysAsync(journeySearchModel.OriginId, journeySearchModel.DestinationId, journeySearchModel.DepartureDate);
        
        return View(journeys);
    }
}