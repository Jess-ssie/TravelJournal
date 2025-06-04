using System;
using System.Collections.Generic;
using TravelJournal.Models;
using TravelJournal.Services;

namespace TravelJournal.Commands
{
    public class HistoryCommand : ICommand
    {
        private readonly CommandManager _manager;

        public HistoryCommand(CommandManager manager)
        {
            _manager = manager;
        }

        public void Execute(string[] args)
        {
            List<History> histories = _manager.GetHistory();
            if (histories.Count == 0)
            {
                Console.WriteLine("[SUCCESS] Command history is empty.");
                return;
            }

            Console.WriteLine("[SUCCESS] Command history:");
            for (int i = 0; i < histories.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {histories[i].StartTime:yyyy-mm-dd} {histories[i].Command}");
            }
        }
    }
}
