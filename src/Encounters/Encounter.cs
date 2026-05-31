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
            Output.Handler.WriteLine("");

            Output.Handler.WriteLine(EnemiesEncountered());
            Output.Handler.WriteLine("");
            PrintEnemiesHP();

            CombatManager combat = new();
            combat.HandleCombat(context, Enemies);
        }

        private void PrintEnemiesHP()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Output.Handler.WriteLine(Enemies[i].Name + " HP: " + $"{Enemies[i].Stats.CurrentHealth}");
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

    public class ItemEncounter : IEncounter
    {
        Consumable Item;

        public ItemEncounter(Consumable item)
        {
            Item = item;
        }

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

    public class TrapEncounter : IEncounter
    {
        public int Damage { get; }

        public TrapEncounter(int damage)
        {
            Damage = damage;
        }

        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("You fell into a trap!");

            context.Player.TakeDamage(Damage);
            context.AddLog(new TrapEscapedLog(context.Player.Name, Damage));

            context.Player.PrintCurrentHP();
        }
    }

    public class HealingFountainEncounter : IEncounter
    {
        public int HPRestoration;

        public HealingFountainEncounter(int hpRestoration)
        {
            HPRestoration = hpRestoration;
        }

        public void Execute(GameContext context)
        {
            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("You see a fountain, the water in which magically glows.");

            context.Player.RestoreHP(HPRestoration);
            context.AddLog(new FountainWaterSipLog(context.Player.Name, HPRestoration));

            context.Player.PrintCurrentHP();
        }
    }

    public class ShrineEncounter : IEncounter
    {
        private const int Damage = 30;
        private const int Restoration = 30;
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
