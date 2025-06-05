using System;
using System.Collections.Generic;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;
using TravelJournal.Services;
using TravelJournal.Enums;

namespace TravelJournal.Commands;

public class ListLocationsSortCommand : ICommand
{
    private readonly LocationRepository _locationRepository;
    public ListLocationsSortCommand(LocationRepository service) => _locationRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: list-locations-sort <sort> {sort = dateUp | dateDown}");
            return;
        }
        DateSort sort = DateSort.DateDown;
        try
        {
            sort = Enum.Parse<DateSort>(args[0], ignoreCase: true);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[ARGUMENT ERROR] {ex.Message} sort = [ DateUp | DateDown ]");
            return;
        }
        List<Location> locations = GetAllLocationsSort(sort);
        if (!locations.Any())
        {
            Console.WriteLine("[SUCCESS] Location List empty");
            return;
        }
        // Ширина колонок
        int idWidth = 4;
        int countryWidth = 20;
        int cityWidth = 20;
        int dateWidth = 12;
        int tripIdWidth = 8;
        // Заголовок
        Console.WriteLine(
            "ID".PadRight(idWidth) + " | " +
            "Country".PadRight(countryWidth) + " | " +
            "City".PadRight(cityWidth) + " | " +
            "Visit Date".PadRight(dateWidth) + " | " +
            "Trip ID".PadRight(tripIdWidth));

        Console.WriteLine(new string('-', idWidth + countryWidth + cityWidth + dateWidth + tripIdWidth + 13)); // 13 для " | " роздільників

        foreach (Location location in locations)
        {
            Console.WriteLine(
                location.Id.ToString().PadRight(idWidth) + " | " +
                location.Country.PadRight(countryWidth) + " | " +
                location.City.PadRight(cityWidth) + " | " +
                location.VisitDate.ToString("yyyy-MM-dd").PadRight(dateWidth) + " | " +
                location.TripId.ToString().PadRight(tripIdWidth));
        }
    }

    public List<Location> GetAllLocationsSort(DateSort sort)
    {
        return _locationRepository.FindAllSort(sort);
    }

}
