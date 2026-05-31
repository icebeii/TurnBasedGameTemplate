using src.Core;
using src.Effects;
using src.Entities;
using src.Items;
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
            target.TakeDamage(damage);

            return new DamageLog(actor.Name, target.Name, damage);
        }
    }

    public class DefendAction : ICombatAction {
        public IGameLog PerformAction(Entity actor, Entity? target)
        {
            DefenseBuff effect = new DefenseBuff(1, actor.Stats.Defense);
            actor.ApplyEffect(effect);

            return new EffectLog(actor.Name, effect.EntityState);
        }
    }

    public class UseItemAction : ICombatAction
    {
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
            return new EmptyLog();
        }
    }
}
