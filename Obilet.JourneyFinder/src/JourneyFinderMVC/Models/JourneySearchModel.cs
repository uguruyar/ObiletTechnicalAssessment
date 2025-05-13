namespace JourneyFinderMVC.Models;

public class JourneySearchModel
{
    public string Origin { get; set; }
    public int OriginId { get; set; }
    public string Destination { get; set; }
    public int DestinationId { get; set; }
    public DateTime DepartureDate { get; set; }
}