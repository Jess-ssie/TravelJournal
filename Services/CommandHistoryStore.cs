using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TravelJournal.Models;

namespace TravelJournal.Services;

public class CommandHistoryStore
{
    private const string _filePath = "Data/command-history.json";

    public void Save(List<History> history)
    {
        string json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<History> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<History>();
        }

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<History>>(json) ?? new List<History>();
    }

    public void Clear()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }
}
