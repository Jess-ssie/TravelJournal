using System;

namespace TravelJournal.Models;

public abstract class ModelObject
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }
 }
