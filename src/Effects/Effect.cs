using System;
using System.Collections.Generic;

namespace src.Effects
{
    /// <summary>
    /// Represents a temporary effect applied to an entity, that modifies its stats or behavior for a limited duration
    /// </summary>
    public abstract class Effect
    {
        /// <summary>
        /// Gets the remaining duration of the effect in turns.
        /// </summary>
        public int Duration { get; protected set; }

        /// <summary>
        /// Gets a textual representation of the entity's current state caused by this effect
        /// </summary>
        public abstract string EntityState { get; }

        /// <summary>
        /// Reduces the remaining duration of the effect by one turn.
        /// </summary>
        public void DecreaseDuration()
        {
            Duration--;
        }

        /// <summary>
        /// Determines whether the effect has expired
        /// </summary>
        public bool IsExpired() => Duration <= 0;
    }

    /// <summary>
    /// Represents a temporary defensive buff that increases an entity's defense value
    /// </summary>
    public class DefenseBuff : Effect
    {
        /// <summary>
        /// Gets the additional defense provided by this effect
        /// </summary>
        public int DefenseBonus { get; }

        /// <summary>
        /// Gets the visual state description for a defending entity
        /// </summary>
        public override string EntityState => "is defending";

        /// <summary>
        /// Initializes a new instance of the <see cref="DefenseBuff"/> class
        /// </summary>
        /// <param name="duration">How many turns the effect lasts</param>
        /// <param name="defenseBonus">Amount of defense added while active</param>
        public DefenseBuff(int duration, int defenseBonus)
        {
            Duration = duration;
            DefenseBonus = defenseBonus;
        }
    }

    /// <summary>
    /// Represents a temporary attack buff that increases an entity's attack power for a limited time
    /// </summary>
    public class AttackBuff : Effect
    {
        /// <summary>
        /// Gets the additional attack provided by this effect
        /// </summary>
        public int AttackBonus { get; }

        /// <summary>
        /// Gets the visual state description for a strengthened entity
        /// </summary>
        public override string EntityState => "is full of strength";

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackBuff"/> class
        /// </summary>
        /// <param name="attackBonus">Amount of attack increase</param>
        /// <param name="duration">How many turns the effect lasts</param>
        public AttackBuff(int attackBonus, int duration)
        {
            AttackBonus = attackBonus;
            Duration = duration;
        }
    }
}
