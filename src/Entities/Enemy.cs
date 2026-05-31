using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace src.Entities
{
    public class Enemy : Entity
    {
        public int XPReward;
        public Enemy(string name, int maxHealth, int attack, int defense, int xpReward)
        {
            Name = name;
            Stats = new Stats
            {
                MaxHealth = maxHealth,
                CurrentHealth = maxHealth,
                Attack = attack,
                Defense = defense
            };
            XPReward = xpReward;
        }
    }

    public class Goblin : Enemy
    {
        private const int HPPerLevel = 8;
        private const int AttackPerLevel = 3;
        private const int DefensePerLevel = 2;
        public Goblin(int level) : base("Goblin", 30 + level * HPPerLevel, 
            8 + level * AttackPerLevel, 
            2 + level * DefensePerLevel, 
            20 * level) { }
    }

    public class Spider : Enemy
    {
        private const int HPPerLevel = 5;
        private const int AttackPerLevel = 10;
        private const int DefensePerLevel = 1;
        public Spider(int level) : base("Spider", 20 + level * HPPerLevel, 
            10 + level * AttackPerLevel, 
            4 + level * DefensePerLevel, 
            40 * level) { }
    }

    public class Skeleton : Enemy
    {
        private const int HPPerLevel = 6;
        private const int AttackPerLevel =6;
        private const int DefensePerLevel = 4;
        public Skeleton(int level) : base("Skeleton", 15 + level * HPPerLevel, 
            12 + level * AttackPerLevel, 
            3 + level * DefensePerLevel, 
            30 * level) { }
    }
}
