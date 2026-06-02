using src.Core;
using src.Effects;
using System;
using System.Collections.Generic;

namespace src.Entities
{
    /// <summary>
    /// Represents a base game entity that has a name, stats, effects, and health
    /// All characters in the game inherit from this class
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the entity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the combat statistics of the entity
        /// </summary>
        public Stats Stats { get; protected set; }

        /// <summary>
        /// Gets the list of active effects currently applied to the entity
        /// </summary>
        public List<Effect> Effects { get; }

        /// <summary>
        /// Indicates whether the entity is alive
        /// </summary>
        public bool IsAlive => Stats.CurrentHealth > 0;
        protected Entity()
        {
            Effects = new List<Effect>();
        }

        /// <summary>
        /// Applies a new effect to the entity
        /// </summary>
        /// <param name="effect">The effect to apply</param>
        public virtual void ApplyEffect(Effect effect)
        {
            Effects.Add(effect);
        }

        /// <summary>
        /// Reduces the entity's current health by the specified damage amount
        /// Health is clamped to a minimum of 0
        /// </summary>
        /// <param name="damage">The amount of damage to apply</param>
        public virtual void TakeDamage(int damage)
        {
            Stats.CurrentHealth -= damage;

            if (Stats.CurrentHealth < 0)
            {
                Stats.CurrentHealth = 0;
            }
        }

        /// <summary>
        /// Restores the entity's health by the specified amount
        /// Health is clamped to the entity's maximum health value
        /// </summary>
        /// <param name="hpRestoration">The amount of health to restore</param>
        public virtual void RestoreHP(int hpRestoration)
        {
            Stats.CurrentHealth += hpRestoration;

            if (Stats.CurrentHealth > Stats.MaxHealth)
            {
                Stats.CurrentHealth = Stats.MaxHealth;
            }
        }

        /// <summary>
        /// Prints the current and maximum HP of the entity to the output
        /// </summary>
        public virtual void PrintCurrentHP()
        {
            Output.Handler.WriteLine($"{Name} " + "HP: " + $"{Stats.CurrentHealth}" + "/" + $"{Stats.MaxHealth}");
        }
    }
}
