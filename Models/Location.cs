using System;

namespace TravelJournal.Models;

public class Location : ModelObject
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime VisitDate { get; set; }
    public int TripId { get; set; }
}
