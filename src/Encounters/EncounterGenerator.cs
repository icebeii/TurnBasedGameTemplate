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
            int difficulty = context.Player.Level;

            int combat_scale = 5;
            int trap_scale = 2;

            int combat = 40 + difficulty * combat_scale;
            int weapon = 15;
            int item = 20;
            int trap = 15 + difficulty * trap_scale;
            int heal = 10;
            int shrine = 5;

            int sum = combat + weapon + item + trap + heal + shrine;
            int roll = context.Random.Next(sum + 1);

            if ((roll -= combat) < 0)
            {
                return GenerateCombat(context.Random, context.Player.Level);
            }
            if ((roll -= weapon) < 0)
            {
                return GenerateWeaponStand(context.Random);
            }
            if ((roll -= item) < 0)
            {
                return GenerateItemEncounter(context.Random);
            }
            if ((roll -=  trap) < 0)
            {
                return GenerateTrap();
            }
            if ((roll -= heal) < 0)
            {
                return GenerateHealingFountain();
            }
            return GenerateShrine();
        }

        private CombatEncounter GenerateCombat(Random random, int playerLevel)
        {
            int maxEnemies = 5;
            List<Enemy> enemies = new();

            int currentMax = 1 + playerLevel / 4;
            currentMax = Math.Min(currentMax, maxEnemies);
            int count = random.Next(1, currentMax + 1);
            
            for (int i = 0; i < count; i++)
            {
                enemies.Add(GenerateEnemy(random, playerLevel));
            }

            return new CombatEncounter(enemies);
        }

        private Enemy GenerateEnemy(Random random, int playerLevel)
        {
            int roll = random.Next(3);
            switch (roll)
            {
                case 0:
                    return new Goblin(playerLevel - 1);
                case 1:
                    return new Spider(playerLevel - 1);
                case 2:
                    return new Skeleton(playerLevel - 1);
                default:
                    return new Goblin(playerLevel - 1);
            }
        }

        private WeaponStandEncounter GenerateWeaponStand(Random random)
        {
            int maxCount = 3;
            int noWeaponChance = 3;

            int count = random.Next(1, maxCount  + 1);
            List<Weapon?> weapons = new();

            bool allEmpty = true;
            for (int i = 0; i < count; i++)
            {
                Weapon? weapon = null;
                int roll = random.Next(noWeaponChance);
                if (roll != 0)
                {
                    weapon = GenerateWeapon(random);
                    allEmpty = false;
                }

                if (i == count - 1 && allEmpty)
                {
                    weapon = GenerateWeapon(random);
                }
                weapons.Add(weapon);
            }

            return new WeaponStandEncounter(weapons);
        }

        private Weapon GenerateWeapon(Random random)
        {
            int roll = random.Next(3);
            switch (roll)
            {
                case 0:
                    return new Knife();
                case 1:
                    return new Axe();
                case 2:
                    return new Sword();
                default:
                    return new Knife();
            }
        }

        private TrapEncounter GenerateTrap()
        {
            return new TrapEncounter(10);
        }

        private HealingFountainEncounter GenerateHealingFountain()
        {
            return new HealingFountainEncounter(10);
        }

        private ItemEncounter GenerateItemEncounter(Random random)
        {
            int roll = random.Next(2);
            Consumable item = new HealingPotion();
            if (roll == 1)
            {
                item = new StrangeFruit();
            }

            return new ItemEncounter(item);
        }

        private ShrineEncounter GenerateShrine()
        {
            return new ShrineEncounter();
        }
    }
}