using TravelJournal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace TravelJournal.Repositories;

public class TripRepository : DataRepository<Trip>
{

    private readonly string _filePath = "Data/trips.json";
    private List<Trip> _trips;
    private readonly TripNoteRepository _noteRepository;
    private readonly LocationRepository _locationRepository;


    public TripRepository(TripNoteRepository noteRepository, LocationRepository locationRepository)
    {
        _noteRepository = noteRepository;
        _locationRepository = locationRepository;

        if (!File.Exists(_filePath))
        {
            _trips = new List<Trip>();
            SaveChanges();
        }
        else
        {
            string json = File.ReadAllText(_filePath);
            _trips = JsonSerializer.Deserialize<List<Trip>>(json) ?? new List<Trip>();
        }

    }

    public override Trip FindById(int id)
    {
        Trip trip = _trips.FirstOrDefault(t => t.Id == id);

        if (trip == null)
        {
            Console.WriteLine($"[ERROR] Trip with ID \"{id}\" not found.");
            return trip;
        }
        List<string> tripNotes = _noteRepository.FindAllByTripId(trip.Id);
        List<Location> tripLocation = _locationRepository.FindAllByTripId(trip.Id);
        trip.Notes = tripNotes;
        trip.Locations = tripLocation;

        return trip;
    }

    public Trip FindByTitle(string title)
    {
        Trip trip = _trips.FirstOrDefault(t => t.Title == title);

        if (trip == null)
        {
            Console.WriteLine($"[ERROR] Trip with Title \"{title}\" not found.");
            return trip;
        }
        List<string> tripNotes = _noteRepository.FindAllByTripId(trip.Id);
        List<Location> tripLocation = _locationRepository.FindAllByTripId(trip.Id);
        trip.Notes = tripNotes;
        trip.Locations = tripLocation;

        return trip;
    }

    public override List<Trip> FindAll()
    {
        return _trips.ToList();
    }

    public List<Trip> FindAllDetail()
    {
        List<Trip> trips = _trips.ToList();
        foreach (Trip trip in trips)
        {
            List<string> tripNotes = _noteRepository.FindAllByTripId(trip.Id);
            List<Location> tripLocation = _locationRepository.FindAllByTripId(trip.Id);
            trip.Notes = tripNotes;
            trip.Locations = tripLocation;
        }

        return trips;
    }

    public override void Insert(Trip item)
    {
        item.Id = _trips.Count == 0 ? 1 : _trips.Max(t => t.Id) + 1;
        _trips.Add(item);
        SaveChanges();
        Console.WriteLine($"[SUCCESS] Trip added.");
    }

    public void SaveChanges()
    {
        string json = JsonSerializer.Serialize(_trips, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public override void Update(Trip item)
    {
        int index = _trips.FindIndex(t => t.Id == item.Id);
        if (index >= 0)
        {
            _trips[index] = item;
            SaveChanges();
            Console.WriteLine($"[SUCCESS] Trip updated.");
            return;
        }
        Console.WriteLine($"[ERROR] Trip with ID:{item.Id} not found.");

    }

    public override void Delete(int itemId)
    {
        int index = _trips.FindIndex(n => n.Id == itemId);
        if (index != -1)
        {
            _noteRepository.DeleteAllByTripId(itemId);
            _locationRepository.DeleteAllByTripId(itemId);
            _trips.RemoveAt(index);
            SaveChanges();
            Console.WriteLine($"[SUCCESS] Trip deleted.");
            return;
        }
        Console.WriteLine($"[ERROR] Trip with ID:{itemId} not found.");

    }

    public List<Trip> FindByCountry(string country)
    {
        List<int> tripsId = _locationRepository.FindAllByCountry(country);
        List<Trip> trips = _trips
        .Where(trip => tripsId.Contains(trip.Id))
        .Select(trip =>
        {
            trip.Notes = _noteRepository.FindAllByTripId(trip.Id);
            trip.Locations = _locationRepository.FindAllByTripId(trip.Id);
            return trip;
        })
        .ToList();

        return trips;
    }
}
