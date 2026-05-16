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

        public GameContext(Player player)
        {
            Player = player;
            Random = new Random();
        }
    }
}
