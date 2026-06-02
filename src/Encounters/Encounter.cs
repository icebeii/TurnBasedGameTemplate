using src.Combat;
using src.Core;
using src.Entities;
using src.Items;
using System;
using System.Collections.Generic;

namespace src.Encounters
{
    /// <summary>
    /// Represents a game encounter that the player can experience during the game
    /// </summary>
    public interface IEncounter
    {
        /// <summary>
        /// Executes the encounter logic using the provided game context
        /// </summary>
        /// <param name="context">The current game context</param>
        void Execute(GameContext context);
    }

    /// <summary>
    /// Represents a combat encounter where the player fights one or more enemies
    /// </summary>
    public class CombatEncounter : IEncounter
    {
        /// <summary>
        /// Gets the list of enemies participating in the encounter
        /// </summary>
        public List<Enemy> Enemies { get; }

        public CombatEncounter(List<Enemy> enemies)
        {
            Enemies = enemies;
        }

        /// <summary>
        /// Executes a combat encounter, displaying enemies and starting the combat loop
        /// </summary>
        /// <param name="context">The current game context</param>
        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");

            Output.Handler.WriteLine(EnemiesEncountered());
            Output.Handler.WriteLine("");
            PrintEnemiesHP();

            CombatManager combat = new();
            combat.HandleCombat(context, Enemies);
        }

        /// <summary>
        /// For each enemy in list prints its HP
        /// </summary>
        private void PrintEnemiesHP()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Output.Handler.WriteLine(Enemies[i].Name + " HP: " + $"{Enemies[i].Stats.CurrentHealth}");
            }
        }

        /// <summary>
        /// Generates a string with a list of encountered enemies
        /// </summary>
        /// <returns>a string with enemieas list which can be written to output</returns>
        private string EnemiesEncountered()
        {
            string output = "You encountered ";

            for (int i = 0; i < Enemies.Count; i++)
            {
                output += Enemies[i].Name;

                if (i < Enemies.Count - 2)
                {
                    output += ", ";
                }
                else if (i == Enemies.Count - 2)
                {
                    output += " and ";
                }
            }
            output += "!";

            return output;
        }
    }

    /// <summary>
    /// Represents an encounter where the player can inspect and equip weapons from weapon stands
    /// </summary>
    public class WeaponStandEncounter : IEncounter
    {
        /// <summary>
        /// Gets the list of available weapons on the stands. A value of <c>null</c> represents an empty stand
        /// </summary>
        public List<Weapon?> Weapons { get; }

        public WeaponStandEncounter(List<Weapon?> weapons)
        {
            Weapons = weapons;
        }

        /// <summary>
        /// Executes the weapon stand encounter
        /// </summary>
        /// <param name="context">The current game context</param>
        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine($"You found a room with {Weapons.Count} weapon stands.");

            int choice = 0;
            while (choice != Weapons.Count + 1)
            {
                PrintWeaponList();

                choice = InputHandler.GetChoiceFromTheList(1, Weapons.Count + 1);
                if (choice == Weapons.Count + 1)
                {
                    break;
                }

                PickWeapon(context, choice - 1);
                
            }
        }

        /// <summary>
        /// Handles equipping the new weapon and swapping with the previously equipped one
        /// </summary>
        /// <param name="context">The current game context</param>
        /// <param name="index">Index of the weapon stand to interact with</param>
        public void PickWeapon(GameContext context, int index)
        {
            if (index < 0 ||  index >= Weapons.Count)
            {
                return;
            }

            Weapon? selected = Weapons[index];
            if (selected != null)
            {
                context.AddLog(selected.Equip(context.Player));
            }

            Weapon? previous = context.Player.Equipment.EquipWeapon(selected);
            if (previous != null)
            {
                context.AddLog(new WeaponDroppedLog(previous.Name, context.Player.Name));
            }
            Weapons[index] = previous;
        }

        /// <summary>
        /// Prints a list of proposed weapons
        /// </summary>
        private void PrintWeaponList()
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("Choose a weapon:");
            for (int i = 0; i < Weapons.Count;i++)
            {
                if ( Weapons[i] != null)
                {
                    Output.Handler.WriteLine($"{i + 1}. {Weapons[i].Name}: {Weapons[i].StatsBonus.Attack} attack bonus");
                }
                else
                {
                    Output.Handler.WriteLine($"{i + 1}. This stand is empty");
                }
            }
            Output.Handler.WriteLine($"{Weapons.Count + 1}. Exit the room");
        }
    }

    /// <summary>
    /// Represents an encounter where the player finds a consumable item
    /// </summary>
    public class ItemEncounter : IEncounter
    {
        Consumable Item;

        public ItemEncounter(Consumable item)
        {
            Item = item;
        }

        /// <summary>
        /// Executes the item encounter, allowing the player to take or ignore the item
        /// </summary>
        /// <param name="context">The current game context</param>
        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine($"You enter a quiet room. A {Item.Name} lies on the ground.");

            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("1. Take it");
            Output.Handler.WriteLine("2. Exit the room");

            int choice = InputHandler.GetChoiceFromTheList(1, 2);
            switch (choice)
            {
                case 1:
                    IGameLog log = TakeItem(context.Player);
                    context.AddLog(log);
                    break;
                case 2:
                    return;
            }
        }

        /// <summary>
        /// Handles the item picking logic
        /// </summary>
        /// <param name="player">The player who takes the item</param>
        /// <returns>A game log with information if the item was succesfully picked up or not</returns>
        private IGameLog TakeItem(Player player)
        {
            bool success = player.TakeItem(Item);
            if (success)
            {
                return new ItemWasTakenLog(player.Name, Item.Name);
            }
            else
            {
                return new FailedTakeItemLog(player.Name, Item.Name);
            }
        }
    }

    /// <summary>
    /// Represents a trap encounter that deals immediate damage to the player.
    /// </summary>
    public class TrapEncounter : IEncounter
    {
        /// <summary>
        /// Gets the amount of damage for this encounter
        /// </summary>
        public int Damage { get; }

        public TrapEncounter(int damage)
        {
            Damage = damage;
        }

        /// <summary>
        /// Executes the trap encounter, applying damage to the player and logging the result
        /// </summary>
        /// <param name="context">The current game context</param>
        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("You fell into a trap!");

            context.Player.TakeDamage(Damage);
            context.AddLog(new TrapEscapedLog(context.Player.Name, Damage));

            context.Player.PrintCurrentHP();
        }
    }

    /// <summary>
    /// Represents an encounter where the player can restore health at a magical fountain
    /// </summary>
    public class HealingFountainEncounter : IEncounter
    {
        /// <summary>
        /// The amount of HP restoration for this encounter
        /// </summary>
        public int HPRestoration { get; }

        public HealingFountainEncounter(int hpRestoration)
        {
            HPRestoration = hpRestoration;
        }

        /// <summary>
        /// Executes the healing fountain encounter, restoring player HP
        /// </summary>
        /// <param name="context">The current game context</param>
        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("You see a fountain, the water in which magically glows.");

            context.Player.RestoreHP(HPRestoration);
            context.AddLog(new FountainWaterSipLog(context.Player.Name, HPRestoration));

            context.Player.PrintCurrentHP();
        }
    }

    /// <summary>
    /// Represents a shrine encounter where the player chooses between receiving a blessing or a curse
    /// </summary>
    public class ShrineEncounter : IEncounter
    {
        private const int Damage = 30;
        private const int Restoration = 30;

        /// <summary>
        /// Executes the shrine encounter, applying either a blessing or a curse based on player choice
        /// </summary>
        /// <param name="context">The current game context</param>
        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("A strange shrine stands before you.");
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("1. Pray");
            Output.Handler.WriteLine("2. Ignore");

            int choice = InputHandler.GetChoiceFromTheList(1, 2);
            if (choice == 1)
            {
                context.Player.RestoreHP(Restoration);
                context.AddLog(new BlessedByShrineLog(context.Player.Name, Restoration));
            }
            else if (choice == 2)
            {
                context.Player.TakeDamage(Damage);
                context.AddLog(new CursedByShrineLog(context.Player.Name, Damage));
            }
            context.Player.PrintCurrentHP();
        }
    }
}
