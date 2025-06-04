using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TravelJournal.Models;
using System.Linq;
using System;

namespace TravelJournal.Repositories;

public class TripNoteRepository : DataRepository<TripNote>
{
    private readonly string _filePath = "Data/trip-notes.json";
    private List<TripNote> _notes;

    private TripRepository _tripRepository;

    public TripNoteRepository()
    {
        if (!File.Exists(_filePath))
        {
            _notes = new List<TripNote>();
            SaveChanges();
        }
        else
        {
            string json = File.ReadAllText(_filePath);
            _notes = JsonSerializer.Deserialize<List<TripNote>>(json) ?? new List<TripNote>();
        }
    }
    private void SaveChanges()
    {
        string json = JsonSerializer.Serialize(_notes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void SetTripRepository(TripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public override TripNote FindById(int id)
    {
        return _notes.FirstOrDefault(n => n.Id == id);
    }

    public override List<TripNote> FindAll()
    {
        return _notes.ToList();
    }

    public List<string> FindAllByTripId(int tripId)
    {
        return _notes.Where(note => note.TripId == tripId)
        .Select(note => note.Note)
        .ToList();
    }

    public override void Insert(TripNote item)
    {
        Trip trip = _tripRepository.FindById(item.TripId);
        if (trip == null)
        {
            return;
        }
        item.Id = _notes.Count == 0 ? 1 : _notes.Max(n => n.Id) + 1;
        _notes.Add(item);
        SaveChanges();
        Console.WriteLine($"[SUCCESS] Note added.");
    }

    public override void Update(TripNote item)
    {
        int index = _notes.FindIndex(n => n.Id == item.Id);
        if (index != -1)
        {
            _notes[index] = item;
            SaveChanges();
            Console.WriteLine($"[SUCCESS] Note updated.");
            return;
        }
        Console.WriteLine($"[ERROR] Note with ID \"{item.Id}\" not found.");
    }

    public override void Delete(int itemId)
    {
        int index = _notes.FindIndex(n => n.Id == itemId);
        if (index != -1)
        {
            _notes.RemoveAt(index);
            SaveChanges();
            Console.WriteLine($"[SUCCESS] Note deleted.");
            return;
        }
        Console.WriteLine($"[ERROR] Note with ID \"{itemId}\" not found.");
    }

    public void DeleteAllByTripId(int tripId)
    {
        int countBefore = _notes.Count;

        _notes.RemoveAll(note => note.TripId == tripId);
        SaveChanges();

        int countAfter = _notes.Count;
        int deletedCount = countBefore - countAfter;

        Console.WriteLine($"[SUCCESS] Deleted {deletedCount} note(s) related to Trip ID \"{tripId}\".");
    }
}
