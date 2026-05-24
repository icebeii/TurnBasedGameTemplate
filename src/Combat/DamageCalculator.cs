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
            int damage = actor.Stats.Attack - defense; 
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
    }
}