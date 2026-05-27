using src.Core;
using src.Entities;
using src.Items;
using System;
using System.Collections.Generic;

namespace src.Encounters
{
    public class EncounterGenerator
    {
        public IEncounter Generate(GameContext context)
        {
            int roll = context.Random.Next(100);

            return GenerateHealingFountain();
        }

        private CombatEncounter GenerateCombat()
        {
            Enemy enemy = new Goblin();
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(enemy);

            return new CombatEncounter(enemies);
        }

        private WeaponStandEncounter GenerateWeaponStand()
        {
            List<Weapon> weapons = new List<Weapon>
            {
                new Axe(),
                new Knife(),
            };
            return new WeaponStandEncounter(weapons);
        }

        private TrapEncounter GenerateTrap()
        {
            return new TrapEncounter(10);
        }

        private HealingFountainEncounter GenerateHealingFountain()
        {
            return new HealingFountainEncounter(10);
        }
    }
}