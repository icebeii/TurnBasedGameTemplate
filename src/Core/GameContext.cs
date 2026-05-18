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

        public GameContext(Player player)
        {
            Player = player;
            Random = new Random();
        }

        public void AddLog(IGameLog log)
        {
            Logs.Add(log);
            Console.WriteLine();
            Console.WriteLine(log.GetMessage());
        }
    }
}
