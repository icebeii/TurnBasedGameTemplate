using src.Core;
using src.Entities;
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
}
