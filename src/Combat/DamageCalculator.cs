using src.Entities;
using src.Effects;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    /// <summary>
    /// Provides utility methods for calculating combat damage between entities
    /// </summary>
    public static class DamageCalculator
    {
        /// <summary>
        /// Calculates the final damage dealt by an attacker to a target entity
        /// Damage is computed as attack minus defense, with a minimum value of 1
        /// </summary>
        /// <param name="actor">The entity performing the attack</param>
        /// <param name="target">The entity receiving the damage</param>
        /// <returns>The final damage value applied to the target</returns>
        public static int CalculateDamage(Entity actor, Entity target)
        {
            int defense = CalculateDefense(target);
            int attack = CalculateAttack(actor);

            int damage = attack - defense; 
            if (damage < 1)
            {
                damage = 1;
            }
            return damage;
        }

        /// <summary>
        /// Calculates the total defense value of an entity, including active effects
        /// Defense buffs are applied and their duration is reduced during calculation
        /// </summary>
        /// <param name="entity">The entity whose defense is being calculated</param>
        /// <returns>The defense value after applying all active effects</returns>

        private static int CalculateDefense(Entity entity)
        {
            int defense = entity.Stats.Defense;
            foreach (Effect effect in entity.Effects)
            {
                if (effect is DefenseBuff defenseBuff)
                {
                    defense += defenseBuff.DefenseBonus;
                    defenseBuff.DecreaseDuration();
                }
            }
            return defense;
        }

        /// <summary>
        /// Calculates the total attack value of an entity, including equipment bonuses and modifiers
        /// If the entity is a player and has a weapon equipped, its bonus stats and damage modifiers are applied
        /// </summary>
        /// <param name="entity">The entity whose attack is being calculated</param>
        /// <returns>The attack value after applying equipment and modifiers</returns>
        private static int CalculateAttack(Entity entity)
        {
            int attack = entity.Stats.Attack;
            if (entity is Player player && player.Equipment.EquippedWeapon != null)
            {
                attack += player.Equipment.EquippedWeapon.StatsBonus.Attack;
                attack = player.Equipment.EquippedWeapon.ModifyDamage(attack);
            }
            return attack;
        }
    }
}