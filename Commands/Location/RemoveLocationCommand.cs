using TravelJournal.Services;
using TravelJournal.Models;
using System;
using TravelJournal.Repositories;
using System.Data.Common;

namespace TravelJournal.Commands;

public class RemoveLocationCommand : ICommand
{
    private readonly LocationRepository locationRepository;
    public RemoveLocationCommand(LocationRepository service) => locationRepository = service;

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
        locationRepository.Delete(id);
    }
}
