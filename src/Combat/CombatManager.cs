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
                
                RemoveDeadEnemies(enemies);
                if (enemies.Count == 0)
                {
                    Console.WriteLine("All enemies defeated!");
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
            Console.WriteLine();
            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AttackAction attack = new AttackAction();
                    IGameLog attackLog = attack.PerformAction(context.Player, enemies[0]);
                    context.AddLog(attackLog);
                    break;
                    
                case "2": 
                    DefendAction defend = new DefendAction();
                    IGameLog defendLog = defend.PerformAction(context.Player, null);
                    context.AddLog(defendLog);
                    break;

                default:
                    return;
            }
        }

        private void EnemyTurn(GameContext context, List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    AttackAction attack = new AttackAction();
                    IGameLog log = attack.PerformAction(enemy, context.Player);
                    context.AddLog(log); 
                }
            }
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

        private void RemoveDeadEnemies(List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].IsAlive)
                {
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
