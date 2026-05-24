using src.Entities;
using src.Effects;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    public static class DamageCalculator
    {
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

        private static int CalculateDefense(Entity entity)
        {
            int defense = entity.Stats.Defense;
            foreach (Effect effect in entity.Effects)
            {
                if (effect is DefenseBuff defenseBuff)
                {
                    defense *= defenseBuff.DefenseMultiplier;
                    defenseBuff.DecreaseDuration();
                }
            }
            return defense;
        }

        private static int CalculateAttack(Entity entity)
        {
            int attack = entity.Stats.Attack;
            if (entity is Player player && player.Equipment.EquippedWeapon != null)
            {
                attack += player.Equipment.EquippedWeapon.StatsBonus.Attack;
            }
            return attack;
        }
    }
}