using src.Core;
using src.Entities;
using src.Items;
using System;
using System.Collections.Generic;

namespace src.Encounters
{
    /// <summary>
    /// Generates random encounters based on weighted probabilities and player level
    /// </summary>
    public class EncounterGenerator
    {
        /// <summary>
        /// Generates a random encounter using weighted probabilities that scale with player level
        /// Higher player levels increase the chance of more difficult encounters
        /// </summary>
        /// <param name="context">The current game</param>
        /// <returns>A randomly selected encounter instance</returns>
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

        /// <summary>
        /// Creates a combat encounter with a number of enemies scaled by player level
        /// </summary>
        /// <param name="random">Random number generator used for enemy count and selection</param>
        /// <param name="playerLevel">Current player level used for scaling difficulty</param>
        /// <returns>A combat encounter containing generated enemies</returns>
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

        /// <summary>
        /// Generates a random enemy type scaled to the player's level
        /// </summary>
        /// <param name="random">Random number generator</param>
        /// <param name="playerLevel">Player level used to scale enemy strength</param>
        /// <returns>A newly created enemy instance</returns>
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

        /// <summary>
        /// Generates a weapon stand encounter containing a random selection of weapons or empty slots
        /// Ensures that at least one weapon is available if all other slots are empty
        /// </summary>
        /// <param name="random">Random number generator used for weapon selection</param>
        /// <returns>A weapon stand encounter</returns>
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

        /// <summary>
        /// Generates a random weapon instance.
        /// </summary>
        /// <param name="random">Random number generator</param>
        /// <returns>A randomly selected weapon</returns>
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

        /// <summary>
        /// Generates a trap encounter that deals fixed damage to the player
        /// </summary>
        private TrapEncounter GenerateTrap()
        {
            return new TrapEncounter(10);
        }

        /// <summary>
        /// Generates a healing fountain encounter that restores a fixed amount of HP
        /// </summary>
        private HealingFountainEncounter GenerateHealingFountain()
        {
            return new HealingFountainEncounter(10);
        }

        /// <summary>
        /// Generates an item encounter where the player may find consumable items
        /// </summary>
        /// <param name="random">Random number generator used to select item type</param>
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

        /// <summary>
        /// Generates a shrine encounter where the player can choose between blessing or curse
        /// </summary>
        private ShrineEncounter GenerateShrine()
        {
            return new ShrineEncounter();
        }
    }
}