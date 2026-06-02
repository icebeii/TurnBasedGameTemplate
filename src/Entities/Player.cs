using src.Core;
using src.Items;
using System;
using System.Collections.Generic;

namespace src.Entities
{
    /// <summary>
    /// Represents the player-controlled character in the game
    /// Manages level progression, experience, inventory, equipment, and base stats
    /// </summary>
    public class Player : Entity
    {
        /// <summary>
        /// Gets the current player level
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Gets the current experience points accumulated toward the next level
        /// </summary>
        public int Experience { get; private set; }

        /// <summary>
        /// Gets the player's inventory for storing consumable items
        /// </summary>
        public Inventory Inventory { get; }

        /// <summary>
        /// Gets the player's equipment slots 
        /// </summary>
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

        /// <summary>
        /// Attempts to add a consumable item to the player's inventory
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>
        /// <c>true</c> if the item was successfully added; otherwise, <c>false</c>
        /// </returns>
        public bool TakeItem(Consumable item)
        {
            if (item != null)
            {
                return Inventory.AddItem(item);
            }
            return false;
        }

        /// <summary>
        /// Adds experience points to the player and handles level-ups if thresholds are reached
        /// When leveling up, player stats are automatically increased
        /// </summary>
        /// <param name="amount">The amount of experience to add</param>
        public void GainXP(int amount)
        {
            int required = CalculateRequiredXP();
            int total = Experience + amount;
            while (total >= required)
            {
                total -= required;
                Level++;
                UpgradeStatsPerLevel();
                Output.Handler.WriteLine($"Level up! Current level: {Level}");
                required = CalculateRequiredXP();
            }
            Experience = total;
        }

        /// <summary>
        /// Displays the current player statistics
        /// </summary>
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

        /// <summary>
        /// Displays the current contents of the player's inventory
        /// If the inventory is empty, a message is shown instead
        /// </summary>
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

        /// <summary>
        /// Calculates the required experience points for the next level
        /// </summary>
        private int CalculateRequiredXP()
        {
            return (int) (100 * Math.Pow(1.5, Level - 1));
        }

        /// <summary>
        /// Increases player stats after leveling up
        /// </summary>
        private void UpgradeStatsPerLevel()
        {
            Stats.MaxHealth += 10;
            Stats.Attack += 2;
            Stats.Defense += 1;
        }
    }
}
