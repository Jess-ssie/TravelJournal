using System;
using TravelJournal.Repositories;

namespace TravelJournal.Commands.Note;

public class RemoveNoteCommand : ICommand
{
    private readonly TripNoteRepository _noteRepository;
    public RemoveNoteCommand(TripNoteRepository service) => _noteRepository = service;

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
        _noteRepository.Delete(id);
    }
}
