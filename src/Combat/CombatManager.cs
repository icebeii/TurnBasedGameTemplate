using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    public class CombatManager
    {
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
