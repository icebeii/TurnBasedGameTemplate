using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Combat
{
    public interface ICombatAction
    {
        IGameLog PerformAction(Entity actor, Entity? target);
    }

    public class AttackAction : ICombatAction
    {
        public IGameLog PerformAction(Entity actor, Entity? target)
        {
            if (target == null)
            {
                return new EmptyLog();
            }
            int damage = DamageCalculator.CalculateDamage(actor, target);
            target.Stats.CurrentHealth -= damage;

            return new DamageLog(actor.Name, target.Name, damage);
        }
    }

    /*
    public class DefendAction : ICombatAction {
        public void PerformAction(Entity actor, Entity? target)
        {
            Console.WriteLine($"{actor.GetType().Name} " + "is defending");
        }
    }*/
}
