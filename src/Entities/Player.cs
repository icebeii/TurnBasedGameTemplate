using src.Items;
using System;
using System.Collections.Generic;

namespace src.Entities
{
    public class Player : Entity
    {
        public int Level { get; private set; }
        public int Experience { get; private set; }

        public Inventory Inventory { get; }

        public EquipmentSlots Equipment { get; }
        public Player()
        {
            Name = "Player";
            Level = 1;
            Experience = 0;
            Inventory = new Inventory(5);
            Equipment = new EquipmentSlots();
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
