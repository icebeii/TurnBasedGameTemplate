using src.Core;
using src.Effects;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Items
{
    /// <summary>
    /// Represents a consumable item that can be used by an entity to produce an effect
    /// </summary>
    public abstract class Consumable : Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Consumable"/> class
        /// </summary>
        /// <param name="name">The name of the consumable item</param>
        protected Consumable(string name) : base(name) { }

        /// <summary>
        /// Uses the consumable item on a target entity and returns a log describing the effect
        /// </summary>
        /// <param name="target">The entity that uses or receives the effect of the item</param>
        /// <returns>A log describing the result of using the item</returns>
        public abstract IGameLog Use(Entity target);
    }

    /// <summary>
    /// A consumable item that temporarily increases the target's attack power
    /// </summary>
    public class StrangeFruit : Consumable
    {
        public int AttackBonus = 5;
        public int EffectDuration = 3;
        public StrangeFruit() : base("strange fruit") { }

        /// <summary>
        /// Applies an attack buff effect to the target entity
        /// </summary>
        /// <param name="target">The entity consuming the fruit</param>
        /// <returns>A log describing the consumed item effect</returns>
        public override IGameLog Use(Entity target)
        {
            target.ApplyEffect(new AttackBuff(AttackBonus, EffectDuration));
            return new StrangeFruitConsumedLog(target.Name, AttackBonus, EffectDuration);
        }
    }

    /// <summary>
    /// A consumable item that restores a fixed amount of health to the target entity
    /// </summary>
    public class HealingPotion : Consumable
    {
        public int HPRestoration = 5;

        public HealingPotion() : base("healing potion") { }

        /// <summary>
        /// Restores health to the target entity
        /// </summary>
        /// <param name="target">The entity consuming the potion</param>
        /// <returns>A log describing the healing effect</returns>
        public override IGameLog Use(Entity target)
        {
            target.RestoreHP(HPRestoration);
            return new HealingPotionConsumedLog(target.Name, HPRestoration);
        }
    }
}
