using TravelJournal.Services;
using TravelJournal.Models;
using System;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class RemoveNoteCommand : ICommand
{
    private readonly TripNoteRepository noteRepository;
    public RemoveNoteCommand(TripNoteRepository service) => noteRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: remove-note <locationId>");
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
        noteRepository.Delete(id);
    }
}
