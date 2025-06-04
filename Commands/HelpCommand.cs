using System;
using System.Collections.Generic;

namespace TravelJournal.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly Dictionary<string, string> _commandDescriptions = new Dictionary<string, string>
        {
            { "help", "help => Показати список доступних команд та опис" },
            { "add-trip", "add-trip <title> <start:yyyy-MM-dd> <finish:yyyy-MM-dd> <state> {state = \"Planned\" | \"InProgress\" | \"Finished\" } => Додати подорож" },
            { "add-trip-note", "add-trip-note <tripId> <note> => Додати нотатку до подорожі" },
            { "add-trip-location", "add-trip-location <tripId> <country> <city> <visitDate:yyyy-MM-dd> => Додати локацію до подорожі" },
            { "list-trips", "list-trips --details => Показати всі подорожі, --detail додає інформацію про нотатки та локації" },
            { "list-locations", "list-locations => Показати всі локації"},
            { "update-trip", "update-trip <tripId> <title> <start:yyyy-MM-dd> <finish:yyyy-MM-dd> <state> {state = \"Planned\" | \"InProgress\" | \"Finished\" } => Оновити існуючі подорож"},
            { "update-trip-location", "update-trip-location <locationId> <tripId> <country> <city> <visitDate:yyyy-MM-dd> => Оновлення Локації" },
            { "find-trip", "find-trip <tripId or title> => Знайти подорож за ID" },
            { "find-trips-by-country", "find-trip-by-country <country> => Знайти подорожі за країною" },
            { "find-locations-by-country-city", "find-locations-by-country-city <country> <city> => Пошук Локації по країні та місті"},
            { "remove-trip", "remove-trip <tripId> => Видалити подорож по ID"},
            { "remove-location" , "remove-location <locationId> => Видалити Локацію по ID"},
            { "command-history", "command-history => Переглянути історію запуску команд"},
            { "command-history-clear", "command-history-clear => Очистити історію команд"}
        };

        public void Execute(string[] args)
        {
            Console.WriteLine("Available commands:");
            foreach (KeyValuePair<string, string> entry in _commandDescriptions)
            {
                Console.Write("    ");
                Console.WriteLine($"{entry.Value}");
            }
        }
    }
}
