using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TravelJournal.Models;
using System.Linq;
using TravelJournal.Enums;

namespace TravelJournal.Repositories;

public class LocationRepository : DataRepository<Location>
{
    private readonly string _filePath = "Data/locations.json";
    private List<Location> _locations;

    private TripRepository _tripRepository;

    public LocationRepository()
    {
        if (!File.Exists(_filePath))
        {
            _locations = new List<Location>();
            SaveChanges();
        }
        else
        {
            string json = File.ReadAllText(_filePath);
            _locations = JsonSerializer.Deserialize<List<Location>>(json) ?? new List<Location>();
        }
    }

    public void SetTripRepository(TripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
    private void SaveChanges()
    {
        string json = JsonSerializer.Serialize(_locations, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_filePath, json);
    }

    public override Location FindById(int id)
    {
        Location location = _locations.FirstOrDefault(l => l.Id == id);
        if (location == null)
        {
            Console.WriteLine($"[ERROR] Trip with ID \"{id}\" not found.");
            return location;
        }
        return location;
    }

    public List<Location> FindAllByCountryAndCity(string country, string city)
    {
        return _locations
            .Where(l =>
                string.Equals(l.Country, country, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(l.City, city, StringComparison.OrdinalIgnoreCase))
                .ToList();

    }

    public override List<Location> FindAll()
    {
        return _locations.ToList();
    }

    public List<Location> FindAllSort(DateSort sort)
    {
        switch (sort)
        {
            case DateSort.DateUp:
                return _locations.OrderBy(location => location.VisitDate).ToList(); // спочатку старіші

            case DateSort.DateDown:
                return _locations.OrderByDescending(location => location.VisitDate).ToList(); // спочатку новіші

            default:
                return _locations.ToList(); // без сортування
        }
    }

    public override void Insert(Location item)
    {
        Trip trip = _tripRepository.FindById(item.TripId);
        if (item.VisitDate > trip.TripFinish || item.VisitDate < trip.TripStart)
        {
            Console.WriteLine("[ERROR] You entered a visit date outside of your travel dates.");
            return;
        }
        item.Id = _locations.Count == 0 ? 1 : _locations.Max(l => l.Id) + 1;
        _locations.Add(item);
        SaveChanges();
        Console.WriteLine($"[SUCCESS] Location added.");
    }

    public override void Update(Location item)
    {
        int index = _locations.FindIndex(l => l.Id == item.Id);
        if (index != -1)
        {
            _locations[index] = item;
            SaveChanges();
            Console.WriteLine($"[SUCCESS] Location updated.");
            return;
        }
        Console.WriteLine($"[ERROR] Location with ID \"{item.Id}\" not found.");
    }

    public override void Delete(int itemId)
    {
        int index = _locations.FindIndex(l => l.Id == itemId);
        if (index != -1)
        {
            _locations.RemoveAt(index);
            SaveChanges();
            Console.WriteLine($"[SUCCESS] Location deleted.");
            return;
        }
        Console.WriteLine($"[ERROR] Location with ID \"{itemId}\" not found.");
    }

    public List<int> FindAllByCountry(string country)
    {
        return _locations.Where(location => location.Country == country).Select(location => location.TripId).ToList();
    }

    public List<Location> FindAllByTripId(int tripId)
    {
        return _locations.Where(location => location.TripId == tripId).OrderBy(location => location.VisitDate).ToList();
    }

    public void DeleteAllByTripId(int tripId)
    {
        int countBefore = _locations.Count;

        _locations.RemoveAll(location => location.TripId == tripId);
        SaveChanges();

        int countAfter = _locations.Count;
        int deletedCount = countBefore - countAfter;

        Console.WriteLine($"[SUCCESS] Deleted {deletedCount} location(s) related to Trip ID: {tripId}.");
    }
}
