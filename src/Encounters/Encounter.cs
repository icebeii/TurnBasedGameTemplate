using src.Combat;
using src.Core;
using src.Entities;
using src.Items;
using System;
using System.Collections.Generic;

namespace src.Encounters
{
    public interface IEncounter
    {
        void Execute(GameContext context);
    }

    public class CombatEncounter : IEncounter
    {
        public List<Enemy> Enemies { get; }
        public CombatEncounter(List<Enemy> enemies)
        {
            Enemies = enemies;
        }

        public void Execute(GameContext context)
        {
            Console.WriteLine();

            Console.WriteLine(EnemiesEncountered());
            Console.WriteLine();
            PrintEnemiesHP();

            CombatManager combat = new();
            combat.HandleCombat(context, Enemies);
        }

        private void PrintEnemiesHP()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Console.WriteLine(Enemies[i].Name + " HP: " + $"{Enemies[i].Stats.CurrentHealth}");
            }
        }

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

    public class WeaponStandEncounter : IEncounter
    {
        public List<Weapon?> Weapons { get; }

        public WeaponStandEncounter(List<Weapon?> weapons)
        {
            Weapons = weapons;
        }

        public void Execute(GameContext context)
        {
            Console.WriteLine();
            Console.WriteLine($"You found a room with {Weapons.Count} weapon stands.");

            int choice = 0;
            while (choice != Weapons.Count + 1)
            {
                PrintWeaponList();

                choice = GetChoice();
                PickWeapon(context, choice - 1);
                
            }
        }

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

        private void PrintWeaponList()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a weapon:");
            for (int i = 0; i < Weapons.Count;i++)
            {
                if ( Weapons[i] != null)
                {
                    Console.WriteLine($"{i + 1}. {Weapons[i].Name}: {Weapons[i].StatsBonus.Attack} attack bonus");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. This stand is empty");
                }
            }
            Console.WriteLine($"{Weapons.Count + 1} Exit the room");
        }

        private int GetChoice()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    if (choice >= 1 && choice <= Weapons.Count)
                    {
                        return choice;
                    }
                }
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
