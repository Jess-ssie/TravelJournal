using System;
using System.Collections.Generic;

namespace TravelJournal.Models;

public class Trip : ModelObject
{
    public string Title { get; set; } = string.Empty;
    public DateTime TripStart { get; set; }
    public DateTime TripFinish { get; set; }
    public TripState State { get; set; }
    public List<string> Notes { get; set; } = new();

    public List<Location> Locations { get; set; } = new();
}

public enum TripState { Planned, InProgress, Finished }
