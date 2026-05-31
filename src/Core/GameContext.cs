using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Core
{
    public class GameContext
    {
        public Player Player { get; }
        public Random Random { get; }
        public int EncounterCount { get; set; }

        public List<IGameLog> Logs { get; } = new();

        public IOutputHandler OutputHandler { get; }

        public GameContext(Player player)
        {
            Player = player;
            Random = new Random();
            OutputHandler = new ConsoleOutputHandler();
        }

        public void AddLog(IGameLog log)
        {
            Logs.Add(log);
            Output.Handler.WriteLine("");
            Output.Handler.ShowLog(log);
        }
    }
}
