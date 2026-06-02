using src.Core;
using src.Effects;
using src.Entities;
using src.Items;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    /// <summary>
    /// Represents an action that can be performed by an entity during combat
    /// </summary>
    public interface ICombatAction
    {
        /// <summary>
        /// Executes the combat action.
        /// </summary>
        /// <param name="actor">The entity performing the action</param>
        /// <param name="target">The target entity of the action, or <c>null</c> if the action does not require a target</param>
        /// <returns>A log entry describing the result of the action</returns>
        IGameLog PerformAction(Entity actor, Entity? target);
    }

    /// <summary>
    /// A combat action that deals damage to a target entity
    /// </summary>
    public class AttackAction : ICombatAction
    {
        /// <summary>
        /// Calculates and applies damage from the attacker to the target
        /// </summary>
        /// <param name="actor">The entity performing the attack</param>
        /// <param name="target">The entity being attacked</param>
        /// <returns>A log entry describing the damage dealt</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> is <c>null</c>
        /// </exception>
        public IGameLog PerformAction(Entity actor, Entity? target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            int damage = DamageCalculator.CalculateDamage(actor, target);
            target.TakeDamage(damage);

            return new DamageLog(actor.Name, target.Name, damage);
        }
    }

    /// <summary>
    /// A combat action that temporarily increases the actor's defense
    /// </summary>
    public class DefendAction : ICombatAction {
        /// <summary>
        /// Applies a temporary defense buff to the acting entity
        /// </summary>
        /// <param name="actor">The entity using the defend action</param>
        /// <param name="target">Unused. This action does not require a target</param>
        /// <returns>A log entry describing the applied effect</returns>
        public IGameLog PerformAction(Entity actor, Entity? target)
        {
            DefenseBuff effect = new DefenseBuff(1, actor.Stats.Defense);
            actor.ApplyEffect(effect);

            return new EffectLog(actor.Name, effect.EntityState);
        }
    }

    /// <summary>
    /// A combat action that allows a player to use a consumable item from the inventory
    /// </summary>
    public class UseItemAction : ICombatAction
    {
        /// <summary>
        /// Prompts the player to select and use an item from their inventory
        /// The used item is removed from the inventory
        /// </summary>
        /// <param name="actor">The entity attempting to use an item</param>
        /// <param name="target">Unused. Item effects determine their own targets</param>
        /// <returns>
        /// A log entry describing the item's effect, or an
        /// <see cref="EmptyInventoryLog"/> if the inventory is empty
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the actor is not a player
        /// </exception>
        public IGameLog PerformAction(Entity actor, Entity? target)
        {
            if (actor is Player player)
            {
                int count = player.Inventory.Items.Count;
                if (count != 0)
                {
                    Output.Handler.WriteLine("Choose an item:");
                    player.PrintInventory();

                    int choice = InputHandler.GetChoiceFromTheList(1, count);
                    Consumable item = player.Inventory.Items[choice - 1];
                    
                    IGameLog log = item.Use(actor);
                    player.Inventory.RemoveItem(choice - 1);    
                    return log;
                }
                else
                {
                    return new EmptyInventoryLog(player.Name);
                }
            }
            throw new InvalidOperationException("Only player can use items.");
        }
    }
}
