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
            Name = "Player";
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

        public void PrintPlayerStats()
        {
            Console.WriteLine();
            Console.WriteLine("Player stats:");
            Console.WriteLine("HP: " + $"{Stats.CurrentHealth}" + "/" + $"{Stats.MaxHealth}");
            Console.WriteLine("Attack: " + $"{Stats.Attack}");
            Console.WriteLine("Defense: " + $"{Stats.Defense}");
            Console.WriteLine("Level: " + $"{Level}");
            Console.WriteLine("XP: " + $"{Experience}");
        }
    }
}
