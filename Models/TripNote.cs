namespace TravelJournal.Models;

public class TripNote : ModelObject
{
    public int TripId { get; set; }
    public string Note { get; set; } = string.Empty;
}
