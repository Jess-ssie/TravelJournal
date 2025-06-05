using System;
using System.Collections.Generic;
using System.Linq;
using TravelJournal.Models;
using TravelJournal.Services;

namespace TravelJournal.Services
{
    public class CommandManager
    {
        private readonly List<History> _commandHistory;
        private readonly CommandHistoryStore _store;

        public CommandManager()
        {
            _store = new CommandHistoryStore();
            _commandHistory = _store.Load();
        }

        public void AddToHistory(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                History history = new History
                {

                    StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Command = input
                };
                _commandHistory.Add(history);
                _store.Save(_commandHistory);
            }
        }

        public List<History> GetHistory() => _commandHistory.ToList();

        public void ClearHistory()
        {
            _commandHistory.Clear();
            _store.Clear();
        }
    }
}
