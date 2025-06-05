using System;
using TravelJournal.Models;
using TravelJournal.Repositories;
using TravelJournal.Services;
using System.Data.Common;
using TravelJournal.Validate;


namespace TravelJournal.Commands;

public class AddTripLocationCommand : ICommand
{
    private readonly LocationRepository _locationRepository;
    public AddTripLocationCommand(LocationRepository service) => _locationRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: add-trip-location <tripId> <country> <city> <visitDate:yyyy-MM-dd>");
            return;
        }
        int tripId = 0;
        string country = args[1];
        string city = args[2];
        DateTime date = DateTime.Now;
        bool isOk = ValidateDate.IsValidDateFormat(args[3]);
        if (!isOk)
        {
            return;
        }
        try
        {
            tripId = int.Parse(args[0]);
            date = DateTime.Parse(args[3]);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[FORMAT ERROR] {ex.Message}");
            return;
        }

        AddTripLocation(tripId, country, city, date);
    }

    public void AddTripLocation(int tripId, string country, string city, DateTime date)
    {
        _locationRepository.Insert(new Location { TripId = tripId, Country = country, City = city, VisitDate = date });
    }
}
