using TravelJournal.Models;
using System;
using TravelJournal.Repositories;
using TravelJournal.Validate;

namespace TravelJournal.Commands;

public class AddTripCommand : ICommand
{
    private readonly TripRepository _tripRepository;
    public AddTripCommand(TripRepository service) => _tripRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: add-trip <title> <start:yyyy-MM-dd> <finish:yyyy-MM-dd> <state>");
            return;
        }

        string title = string.Empty;
        DateTime start = DateTime.Now;
        DateTime finish = DateTime.Now;
        TripState state = TripState.Planned;
        bool isOkStart = ValidateDate.IsValidDateFormat(args[1]);
        if (!isOkStart)
        {
            return;
        }
        bool isOkFinish = ValidateDate.IsValidDateFormat(args[2]);
        if (!isOkFinish)
        {
            return;
        }
        try
        {
            title = args[0];
            start = DateTime.Parse(args[1]);
            finish = DateTime.Parse(args[2]);
            state = Enum.Parse<TripState>(args[3], ignoreCase: true);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[FORMAT ERROR] {ex.Message}");
            return;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[ARGUMENT ERROR] {ex.Message} state = [Planned | InProgress | Finished]");
            return;
        }

        if (finish < start)
        {
            Console.WriteLine("[ERROR] Finish date can not be earlier than Start date");
            return;
        }

        AddTrip(title, start, finish, state);
    }
    public void AddTrip(string title, DateTime start, DateTime finish, TripState state)
    {
        Trip trip = new Trip { Title = title, TripStart = start, TripFinish = finish, State = state };
        _tripRepository.Insert(trip);
    }
}
