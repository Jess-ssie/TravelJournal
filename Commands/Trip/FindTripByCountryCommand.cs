using System;
using System.Collections.Generic;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class FindTripByCountryCommand : ICommand
{
    private readonly TripRepository tripRepository;
    public FindTripByCountryCommand(TripRepository service) => tripRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: find-trips-by-country <country>");
            return;
        }

        string country = string.Join(' ', args);
        List<Trip> trips = FindTripsByCountry(country);
        if (!trips.Any())
        {
            Console.WriteLine("[SUCCESS] Trip List empty");
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
                foreach (Location location in trip.Locations)
                {
                    Console.Write($" => [{location.Country} | {location.City} | {location.VisitDate:yyyy-MM-dd}]");
                }
                Console.WriteLine();
            }
        }
    }

    public List<Trip> FindTripsByCountry(string country)
    {
        return tripRepository.FindByCountry(country);
    }


}
