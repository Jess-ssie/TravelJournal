using System;
using System.Collections.Generic;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;
using TravelJournal.Services;

namespace TravelJournal.Commands;

public class FindLocationCommand : ICommand
{
    private readonly LocationRepository _locationRepository;
    public FindLocationCommand(LocationRepository service) => _locationRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: find-location <locationId>");
            return;
        }
        int id = 0;
        try
        {
            id = int.Parse(args[0]);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[FORMAT ERROR] {ex.Message}");
            return;
        }
        Location location = FindLocation(id);
        Console.Write($"[{location.Id}] {location.Country} | {location.City} | {location.VisitDate:yyyy-MM-dd} | TripId => {location.TripId}");
    }
    public Location FindLocation(int id)
    {
        return _locationRepository.FindById(id);
    }

}
