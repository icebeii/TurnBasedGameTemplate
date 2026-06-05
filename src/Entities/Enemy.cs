using System;
using System.Collections.Generic;

namespace src.Entities
{
    /// <summary>
    /// Represents a base enemy entity with combat stats and experience reward
    /// </summary>
    public abstract class Enemy : Entity
    {
        /// <summary>
        /// Gets the base stats for the enemy
        /// </summary>
        protected abstract Stats BaseStats { get; }

        /// <summary>
        /// Gets the per-level scaling values applied to base stats
        /// </summary>
        protected abstract Stats ScalingStats { get; }

        /// <summary>
        ///  Gets the base experience reward granted when the enemy is defeated
        /// </summary>
        protected abstract int BaseXPReward { get; }

        /// <summary>
        /// Gets the enemy level, which determines stats and reward scaling
        /// </summary>
        protected int Level { get; }

        /// <summary>
        /// Gets the total experience reward granted for defeating the enemy
        /// </summary>
        public int XPReward => BaseXPReward * (Level + 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class
        /// and calculates scaled combat stats based on level
        /// </summary>
        /// <param name="level">The enemy level</param>
        /// <param name="name">The display name of the enemy</param>
        protected Enemy(int level, string name)
        {
            Level = level;
            Name = name;
            Stats = new Stats
            {
                MaxHealth = BaseStats.MaxHealth + level * ScalingStats.MaxHealth,
                CurrentHealth = BaseStats.MaxHealth + level * ScalingStats.MaxHealth,
                Attack = BaseStats.Attack + level * ScalingStats.Attack,
                Defense = BaseStats.Defense + level * ScalingStats.Defense
            };
        }
    }

    /// <summary>
    /// Example enemy instances
    /// </summary>
    public class Goblin : Enemy
    {
        protected override Stats BaseStats => new Stats
        {
            MaxHealth = 30,
            Attack = 8,
            Defense = 2
        };

        protected override Stats ScalingStats => new Stats
        {
            MaxHealth = 8,
            Attack = 3,
            Defense = 2
        };

        protected override int BaseXPReward => 20;

        public Goblin(int level) : base(level, "Goblin") {}
    }

    public class Spider : Enemy
    {
        protected override Stats BaseStats => new Stats
        {
            MaxHealth = 20,
            Attack = 10,
            Defense = 4
        };

        protected override Stats ScalingStats => new Stats
        {
            MaxHealth = 5,
            Attack = 10,
            Defense = 1
        };

        protected override int BaseXPReward => 40;

        public Spider(int level) : base(level, "Spider") {}
    }

    public class Skeleton : Enemy
    {
        protected override Stats BaseStats => new Stats
        {
            MaxHealth = 15,
            Attack = 12,
            Defense = 3
        };

        protected override Stats ScalingStats => new Stats
        {
            MaxHealth = 6,
            Attack = 6,
            Defense = 4
        };

        protected override int BaseXPReward => 30;

        public Skeleton(int level) : base(level, "Skeleton") {}
    }
}
