using System;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class AddTripNoteCommand : ICommand
{
    public readonly TripNoteRepository _noteRepository;
    public AddTripNoteCommand(TripNoteRepository service) => _noteRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: add-trip-note <tripId> <note>");
            return;
        }

        int tripId;
        string note = string.Empty;
        try
        {
            tripId = int.Parse(args[0]);
            note = string.Join(' ', args.Skip(1));
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[FORMAT ERROR] {ex.Message}");
            return;
        }

        AddTripNote(tripId, note);
    }


    public void AddTripNote(int tripId, string note)
    {
        _noteRepository.Insert(new TripNote { TripId = tripId, Note = note });
    }
}
