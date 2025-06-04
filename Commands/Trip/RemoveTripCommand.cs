using TravelJournal.Services;
using TravelJournal.Models;
using System;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class RemoveTripCommand : ICommand
{
    private readonly TripRepository _tripRepository;
    public RemoveTripCommand(TripRepository service) => _tripRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: remove-trip <tripId>");
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
        RemoveTrip(id);
    }

    public void RemoveTrip(int id)
    {
        _tripRepository.Delete(id);
    }
}
