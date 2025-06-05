using System;
using System.Collections.Generic;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class ListTripsCommand : ICommand
{
    private readonly TripRepository _tripRepository;

    public ListTripsCommand(TripRepository service)
    {
        _tripRepository = service;
    }

    public void Execute(string[] args)
    {
        List<Trip> trips;
        if (args.Contains("--details"))
        {
            trips = GetAllTripsDetail();
        }
        else
        {
            trips = GetAllTrips();
        }
        if (!trips.Any())
        {
            Console.WriteLine("[SUCCESS] Trip List empty.");
            return;
        }

        foreach (Trip trip in trips)
        {
            Console.WriteLine($"[{trip.Id}] {trip.Title} | {trip.TripStart:yyyy-MM-dd} - {trip.TripFinish:yyyy-MM-dd} | {trip.State}");
            if (trip.Notes.Any())
            {
                Console.WriteLine($"\tNotes: {string.Join("; ", trip.Notes)}");
            }
            if (trip.Locations.Any())
            {
                Console.Write($"\tTrip Path: ");
                for (int i = 0; i < trip.Locations.Count; i++)
                {
                    Location location = trip.Locations[i];
                    if (i > 0) Console.Write("=>");
                    Console.Write($" [{location.Country} | {location.City} | {location.VisitDate:yyyy-MM-dd}]");
                }
                Console.WriteLine();
            }
        }
    }
    public List<Trip> GetAllTrips()
    {
        return _tripRepository.FindAll();
    }

    public List<Trip> GetAllTripsDetail()
    {
        return _tripRepository.FindAllDetail();
    }

}
