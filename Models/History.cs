using System;

namespace TravelJournal.Models;

public class History : ModelObject
{
    public DateTime StartTime { get; set; }

    public string Command { get; set; }
}

