using System;
using TravelJournal.Services;

namespace TravelJournal.Commands
{
    public class ClearHistoryCommand : ICommand
    {
        private readonly CommandManager _manager;

        public ClearHistoryCommand(CommandManager manager)
        {
            _manager = manager;
        }

        public void Execute(string[] args)
        {
            _manager.ClearHistory();
            Console.WriteLine("[SUCCESS] Command history clean.");
        }
    }
}
