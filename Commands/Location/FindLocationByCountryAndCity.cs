using System;
using System.Collections.Generic;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Repositories;

namespace TravelJournal.Commands;

public class FindLocationByCountryAndCity : ICommand
{
    private readonly LocationRepository _locationRepository;
    public FindLocationByCountryAndCity(LocationRepository service) => _locationRepository = service;

    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: find-location-by-country-city <country> <city>");
            return;
        }
        string country = args[0];
        string city = args[1];
        List<Location> locations = GetAllLocationsByCountryAndCity(country, city);
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
    public List<Location> GetAllLocationsByCountryAndCity(string country, string city)
    {
        return _locationRepository.FindAllByCountryAndCity(country, city);
    }

}
