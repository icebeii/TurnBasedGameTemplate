using System;
using System.Collections.Generic;

namespace src.Entities
{
    public class Player : Entity
    {
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public Player()
        {
            Level = 1;
            Experience = 0;
            Stats = new Stats
            {
                MaxHealth = 100,
                CurrentHealth = 100,
                Attack = 15,
                Defense = 5
            };
        }
    }
}
