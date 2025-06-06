using System;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class FindTripCommand : ICommand
{
    private readonly TripRepository _tripRepository;

    public FindTripCommand(TripRepository service) => _tripRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: find-trip <id or title>");
            return;
        }
        int id = 0;
        string name = args[0];
        bool filterByTitle = false;
        try
        {
            id = int.Parse(args[0]);
        }
        catch (FormatException)
        {
            filterByTitle = true;
        }
        Trip trip = new Trip();
        if (filterByTitle)
        {
            trip = FindTripByTitle(name);
        }
        else
        {
            trip = FindTrip(id);
        }
        if (trip == null)
        {
            return;
        }

        Console.WriteLine(
            $"[{trip.Id}] {trip.Title} | {trip.TripStart:yyyy-MM-dd} - {trip.TripFinish:yyyy-MM-dd} | {trip.State}"
        );
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
                if (i > 0)
                {
                    Console.Write("=>");
                }
                Console.Write(
                    $" [{location.Country} | {location.City} | {location.VisitDate:yyyy-MM-dd}]"
                );
            }
            Console.WriteLine();
        }
    }

    public Trip FindTrip(int id)
    {
        return _tripRepository.FindById(id);
    }

    public Trip FindTripByTitle(string title)
    {
        return _tripRepository.FindByTitle(title);
    }
}
