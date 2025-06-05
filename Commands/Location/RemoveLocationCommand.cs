using System;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class RemoveLocationCommand : ICommand
{
    private readonly LocationRepository _locationRepository;
    public RemoveLocationCommand(LocationRepository service) => _locationRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: remove-location <locationId>");
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
        _locationRepository.Delete(id);
    }
}
