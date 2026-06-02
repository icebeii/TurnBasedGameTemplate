using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    /// <summary>
    /// Manages the combat loop between the player and a group of enemies
    /// Handles turn order, action execution, effect processing, etc
    /// </summary>
    public class CombatManager
    {
        /// <summary>
        /// Runs a full combat encounter until either the player dies or all enemies are defeated
        /// Each loop iteration consists of a player turn, enemy cleanup and enemy turn
        /// </summary>
        /// <param name="context">The current game context</param>
        /// <param name="enemies">The list of enemies participating in the combat</param>
        public void HandleCombat(GameContext context, List<Enemy> enemies)
        {
            while (context.Player.IsAlive && enemies.Count > 0)
            {
                PlayerTurn(context, enemies);
                
                RemoveDeadEnemies(context, enemies);
                if (enemies.Count == 0)
                {
                    context.AddLog(new AllEnemiesDefeatedLog());
                    break;
                }
                foreach (Enemy enemy in enemies)
                {
                    RemoveExpiredEffects(enemy);
                }

                EnemyTurn(context, enemies);
                RemoveExpiredEffects(context.Player);

                PrintHP(context.Player, enemies);
            }
        }

        /// <summary>
        /// Processes the player's turn by prompting them to choose an action
        /// The selected action is executed immediately and logged in the game context
        /// </summary>
        /// <param name="context">The current game context</param>
        /// <param name="enemies">The list of current enemies. The first enemy is used as the default attack target</param>
        private void PlayerTurn(GameContext context, List<Enemy> enemies)
        {
            while (true)
            {
                Output.Handler.WriteLine("");
                Output.Handler.WriteLine("Choose action:");
                Output.Handler.WriteLine("1. Attack");
                Output.Handler.WriteLine("2. Defend");
                Output.Handler.WriteLine("3. Use item");

                int choice = InputHandler.GetChoiceFromTheList(1, 3);
                IGameLog log;

                switch (choice)
                {
                    case 1:
                        AttackAction attack = new();
                        log = attack.PerformAction(context.Player, enemies[0]);
                        context.AddLog(log);
                        return;

                    case 2:
                        DefendAction defend = new();
                        log = defend.PerformAction(context.Player, null);
                        context.AddLog(log);
                        return;

                    case 3:
                        UseItemAction useItem = new();
                        log = useItem.PerformAction(context.Player, null);
                        context.AddLog(log);
                        if (log is EmptyInventoryLog)
                        {
                            continue;
                        }
                        return;
                }
            }
        }

        /// <summary>
        /// Processes all alive enemies' turns by selecting and executing an action for each enemy
        /// Enemy behavior is determined by simple AI logic based on HP and randomness
        /// </summary>
        /// <param name="context">The current game context</param>
        /// <param name="enemies">The list of current enemies</param>
        private void EnemyTurn(GameContext context, List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    ICombatAction action = GetEnemyAction(enemy, context.Random);
                    IGameLog log = action.PerformAction(enemy, context.Player);
                    context.AddLog(log); 
                }
            }
        }

        /// <summary>
        /// Selects an action for an enemy based on its current health and a random roll
        /// Enemies below a certain HP threshold may choose to defend instead of attacking
        /// </summary>
        /// <param name="enemy">The enemy selecting an action</param>
        /// <param name="random">Random generator used to decide behavior</param>
        /// <returns>An <see cref="ICombatAction"/> representing the enemy's chosen action.</returns>
        private ICombatAction GetEnemyAction(Enemy enemy, Random random)
        {
            double lowHPThreshold = 0.4;
            int defenseChance = 40;

            double hpPercent = (double) enemy.Stats.CurrentHealth / enemy.Stats.MaxHealth;
            if (hpPercent < lowHPThreshold)
            {
                int roll = random.Next(100);
                if (roll < defenseChance)
                {
                    return new DefendAction();
                }
            }
            return new AttackAction();
        }

        /// <summary>
        /// Removes all expired status effects from the given entity
        /// </summary>
        /// <param name="entity">The entity whose effects should be cleaned up</param>
        private void RemoveExpiredEffects(Entity entity)
        {
            for (int i = 0; i < entity.Effects.Count; i++)
            {
                if (entity.Effects[i].IsExpired())
                {
                    entity.Effects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes defeated enemies from the encounter, awards XP to the player and logs their defeat
        /// </summary>
        /// <param name="context">The current game context</param>
        /// <param name="enemies">The list of enemies to check for death and removal</param>
        private void RemoveDeadEnemies(GameContext context, List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].IsAlive)
                {
                    context.AddLog(new EnemyDefeatedLog(enemies[i].Name));
                    context.Player.GainXP(enemies[i].XPReward);
                    enemies.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Prints the current HP of the player and all remaining enemies to the console
        /// </summary>
        /// <param name="player">The player whose HP should be displayed</param>
        /// <param name="enemies">The list of enemies whose HP should be displayed</param>
        private void PrintHP(Player player, List<Enemy> enemies)
        {
            Console.WriteLine();
            player.PrintCurrentHP();

            foreach (Enemy enemy in enemies)
            {
                enemy.PrintCurrentHP();
            }
        }
    }
}
