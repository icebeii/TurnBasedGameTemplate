using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace src.Entities
{
    /// <summary>
    /// Represents a base enemy entity with combat stats and experience reward
    /// </summary>
    public class Enemy : Entity
    {
        /// <summary>
        /// Gets the amount of experience points awarded when this enemy is defeated
        /// </summary>
        public int XPReward;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class with specified combat attributes
        /// </summary>
        /// <param name="name">The enemy name</param>
        /// <param name="maxHealth">Maximum and starting health</param>
        /// <param name="attack">Base attack value</param>
        /// <param name="defense">Base defense value</param>
        /// <param name="xpReward">Experience awarded upon defeat</param>
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

    /// <summary>
    /// Example enemy instances
    /// </summary>
    public class Goblin : Enemy
    {
        private const int HPPerLevel = 8;
        private const int AttackPerLevel = 3;
        private const int DefensePerLevel = 2;
        public Goblin(int level) : base("Goblin", 30 + level * HPPerLevel, 
            8 + level * AttackPerLevel, 
            2 + level * DefensePerLevel, 
            20 * (level + 1)) { }
    }

    public class Spider : Enemy
    {
        private const int HPPerLevel = 5;
        private const int AttackPerLevel = 10;
        private const int DefensePerLevel = 1;
        public Spider(int level) : base("Spider", 20 + level * HPPerLevel, 
            10 + level * AttackPerLevel, 
            4 + level * DefensePerLevel, 
            40 * (level + 1)) { }
    }

    public class Skeleton : Enemy
    {
        private const int HPPerLevel = 6;
        private const int AttackPerLevel =6;
        private const int DefensePerLevel = 4;
        public Skeleton(int level) : base("Skeleton", 15 + level * HPPerLevel, 
            12 + level * AttackPerLevel, 
            3 + level * DefensePerLevel, 
            30 * (level + 1)) { }
    }
}
