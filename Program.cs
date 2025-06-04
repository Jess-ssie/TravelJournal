using TravelJournal.Commands;
using TravelJournal.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using TravelJournal.Repositories;

class Program
{
    static void Main(string[] args)
    {

        TripNoteRepository noteRepository = new TripNoteRepository();
        LocationRepository locationRepository = new LocationRepository();
        TripRepository tripRepository = new TripRepository(noteRepository, locationRepository);
        locationRepository.SetTripRepository(tripRepository);
        noteRepository.SetTripRepository(tripRepository);
        CommandManager commandManager = new CommandManager();


        Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>
        {
            { "add-trip", new AddTripCommand(tripRepository) },
            { "add-trip-note", new AddTripNoteCommand(noteRepository) },
            { "add-trip-location", new AddTripLocationCommand(locationRepository) },
            { "list-trips", new ListTripsCommand(tripRepository) },
            { "list-locations", new ListLocationsCommand(locationRepository) },
            { "update-trip", new UpdateTripCommand(tripRepository)},
            { "update-trip-location", new UpdateTripLocationCommand(locationRepository)},
            { "find-trip", new FindTripCommand(tripRepository) },
            { "find-trip-by-country", new FindTripByCountryCommand(tripRepository) },
            { "find-location", new FindLocationCommand(locationRepository)},
            { "find-locations-by-country-city", new FindLocationByCountryAndCity(locationRepository)},
            { "remove-trip", new RemoveTripCommand(tripRepository)},
            { "remove-location", new RemoveLocationCommand(locationRepository)},
            { "command-history", new HistoryCommand(commandManager)},
            { "command-history-clear" , new ClearHistoryCommand(commandManager)},
            { "help", new HelpCommand() }
        };

        if (args.Length == 0)
        {
            Console.WriteLine("Ви не ввели команду. Доступні команди:");
            foreach (string keys in commands.Keys)
                Console.WriteLine($"\t{keys}");
            return;
        }

        string commandKey = args[0].ToLower();

        if (!commands.ContainsKey(commandKey))
        {
            Console.WriteLine($"Команду \"{commandKey}\" не знайдено. Доступні команди:");
            foreach (string commandKeyAvailable in commands.Keys)
                Console.WriteLine($"\t{commandKeyAvailable}");
            return;
        }

        ICommand command = commands[commandKey];

        command.Execute(args.Skip(1).ToArray());
        commandManager.AddToHistory(string.Join(" ", args));

    }

}


