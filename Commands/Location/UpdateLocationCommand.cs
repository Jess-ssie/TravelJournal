using System;
using TravelJournal.Models;
using TravelJournal.Repositories;
using TravelJournal.Services;
using TravelJournal.Validate;

namespace TravelJournal.Commands;

public class UpdateTripLocationCommand : ICommand
{
    private readonly LocationRepository _locationRepository;
    public UpdateTripLocationCommand(LocationRepository service) => _locationRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 5)
        {
            Console.WriteLine("Usage: update-trip-location <locationId> <tripId> <country> <city> <visitDate:yyyy-MM-dd>");
            return;
        }
        string country = args[2];
        string city = args[3];
        int locationId = 0;
        int tripId = 0;
        DateTime date = DateTime.Now;
        bool isOk = ValidateDate.IsValidDateFormat(args[4]);
        if (!isOk)
        {
            return;
        }
        try
        {
            locationId = int.Parse(args[0]);
            tripId = int.Parse(args[1]);

            date = DateTime.Parse(args[4]);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[FORMAT ERROR] {ex.Message}");
            return;
        }

        UpdateTripLocation(locationId, tripId, country, city, date);
    }

    public void UpdateTripLocation(int locationId, int tripId, string country, string city, DateTime date)
    {
        _locationRepository.Update(new Location { Id = locationId, TripId = tripId, Country = country, City = city, VisitDate = date });
    }
}
