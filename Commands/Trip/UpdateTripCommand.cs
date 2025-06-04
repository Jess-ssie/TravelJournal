using TravelJournal.Services;
using TravelJournal.Models;
using System;
using TravelJournal.Repositories;
using System.Data.Common;

namespace TravelJournal.Commands;

public class UpdateTripCommand : ICommand
{
    private readonly TripRepository tripRepository;
    public UpdateTripCommand(TripRepository service) => tripRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 5)
        {
            Console.WriteLine("Usage: update-trip <tripId> <title> <start:yyyy-MM-dd> <finish:yyyy-MM-dd> <state>");
            return;
        }
        int id = 0;
        string title = string.Empty;
        DateTime start = DateTime.Now;
        DateTime finish = DateTime.Now;
        TripState state = TripState.Planned;
        try
        {
            id = int.Parse(args[0]);
            title = args[1];
            start = DateTime.Parse(args[2]);
            finish = DateTime.Parse(args[3]);
            state = Enum.Parse<TripState>(args[4], true);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[FORMAT ERROR] {ex.Message}");
            return;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[ARGUMENT ERROR] {ex.Message}");
            return;
        }

        if (finish < start)
        {
            Console.WriteLine("[ERROR] Finish date can not be earlier than Start date");
            return;
        }
        UpdateTrip(id, title, start, finish, state);
    }

    public void UpdateTrip(int id, string title, DateTime start, DateTime finish, TripState state)
    {
        Trip trip = new Trip { Id = id, Title = title, TripStart = start, TripFinish = finish, State = state };
        tripRepository.Update(trip);
    }
}
