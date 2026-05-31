using src.Core;
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

        public bool TakeItem(Consumable item)
        {
            if (item != null)
            {
                return Inventory.AddItem(item);
            }
            return false;
        }

        public void GainXP(int amount)
        {
            int required = CalculateRequiredXP();
            int total = Experience + amount;

            if (total >= required)
            {
                int rest = total - required;
                Level++;
                Experience = rest;
                UpgradeStatsPerLevel();
                Output.Handler.WriteLine($"Level up! Current level: {Level}");
            }
            else
            {
                Experience = total;
            }
        }

        public void PrintPlayerStats()
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("Player stats:");
            Output.Handler.WriteLine("HP: " + $"{Stats.CurrentHealth}" + "/" + $"{Stats.MaxHealth}");
            Output.Handler.WriteLine("Attack: " + $"{Stats.Attack}");
            Output.Handler.WriteLine("Defense: " + $"{Stats.Defense}");
            Output.Handler.WriteLine("Level: " + $"{Level}");
            Output.Handler.WriteLine("XP: " + $"{Experience}");
        }

        public void PrintInventory()
        {
            string list = Inventory.GetItemList();
            if (list == "")
            {
                Output.Handler.WriteLine("Inventory is empty.");
            }
            else
            {
                Output.Handler.WriteLine(list);
            }
        }

        private int CalculateRequiredXP()
        {
            return (int) (100 * Math.Pow(1.5, Level - 1));
        }

        private void UpgradeStatsPerLevel()
        {
            Stats.MaxHealth += 10;
            Stats.Attack += 2;
            Stats.Defense += 1;
        }
    }
}
