using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Items
{
    /// <summary>
    /// Represents equippable items that provide stat bonuses
    /// </summary>
    public abstract class Equipment : Item
    {
        /// <summary>
        /// Gets the stat bonuses provided by this equipment
        /// </summary>
        public Stats StatsBonus { get; }

        protected Equipment(string name, Stats bonus) : base(name)
        {
            StatsBonus = bonus;
        }

        /// <summary>
        /// Equips this item on the specified entity and returns a log describing the action
        /// </summary>
        /// <param name="entity">The entity equipping the item</param>
        /// <returns>A log describing the equip action</returns>
        public abstract IGameLog Equip(Entity entity);
    }

    /// <summary>
    /// Represents a weapon that can be equipped to modify attack damage
    /// </summary>
    public abstract class Weapon : Equipment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class
        /// </summary>
        /// <param name="name">The weapon name</param>
        /// <param name="bonus">The attack bonus provided by the weapon</param>
        protected Weapon(string name, Stats bonus) : base(name, bonus) {}

        /// <summary>
        /// Equips the weapon on a player entity
        /// Only players are allowed to equip weapons
        /// </summary>
        /// <param name="entity">The entity attempting to equip the weapon</param>
        /// <returns>A log describing the equip action</returns>
        public override IGameLog Equip(Entity entity)
        {
            if (entity is Player player)
            {
                return new WeaponEquippedLog(Name, player.Name, StatsBonus);
            }
            throw new InvalidOperationException("Only player can equip weapons.");
        }

        /// <summary>
        /// Modifies outgoing damage based on weapon-specific behavior
        /// Default implementation does not change damage
        /// </summary>
        /// <param name="damage">The base damage value</param>
        /// <returns>The modified damage value</returns>
        public virtual int ModifyDamage(int damage)
        {
            return damage;
        }
    }

    /// <summary>
    /// A basic weapon with a small attack bonus and no special effects
    /// </summary>
    public class Sword : Weapon
    {
        public Sword() : base("Sword", new Stats { Attack = 5 }) { }
    }

    /// <summary>
    /// A heavy weapon that increases damage with a random bonus
    /// </summary>
    public class Axe : Weapon
    {
        private Random random = new Random();
        public Axe() : base("Axe", new Stats { Attack = 8 }) { }

        /// <summary>
        /// Adds a random damage bonus to the base attack
        /// </summary>
        public override int ModifyDamage(int damage)
        {
            int bonus = random.Next(3, 8);
            return bonus + damage;
        }
    }

    /// <summary>
    /// A weapon with a chance to deal critical damage
    /// </summary>
    public class Knife : Weapon
    {
        private const int CritChance = 30;
        private const double CritMultiplier = 2;
        private Random random = new Random();

        public Knife() : base("Knife", new Stats { Attack = 3 }) { }

        /// <summary>
        /// Applies a chance-based critical hit multiplier
        /// </summary>
        public override int ModifyDamage(int damage)
        {
            int roll = random.Next(100);
            if (roll < CritChance)
            {
                return (int)(damage * CritMultiplier);
            }
            return damage;
        }
    }
}
