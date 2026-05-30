using System;
using System.Collections.Generic;

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
        public Goblin() : base("Goblin", 30, 8, 2, 20) { }
    }

    public class Spider : Enemy
    {
        public Spider() : base("Spider", 20, 10, 4, 40) { }
    }

    public class Skeleton : Enemy
    {
        public Skeleton() : base("Skeleton", 15, 12, 3, 30) { }
    }
}
