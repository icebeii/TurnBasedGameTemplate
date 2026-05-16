using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Encounters
{
    public class EncounterGenerator
    {
        public IEncounter Generate(GameContext context)
        {
            int roll = context.Random.Next(100);

            return GenerateCombat();
        }

        private CombatEncounter GenerateCombat()
        {
            Enemy enemy = new Goblin();
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(enemy);

            return new CombatEncounter(enemies);
        }
    }
}