using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    public static class DamageCalculator
    {
        public static int CalculateDamage(Entity actor, Entity target)
        {
            int defense = target.Stats.Defense;
            int damage = actor.Stats.Attack - defense; 
            return damage;
        }
    }
}