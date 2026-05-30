using src.Core;
using src.Effects;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Items
{
    public abstract class Consumable : Item
    {
        protected Consumable(string name) : base(name) { }

        public abstract IGameLog Use(Entity target);
    }

    public class StrangeFruit : Consumable
    {
        public int AttackBonus = 5;
        public int EffectDuration = 3;
        public StrangeFruit() : base("strange fruit") { }

        public override IGameLog Use(Entity target)
        {
            target.ApplyEffect(new AttackBuff(AttackBonus, EffectDuration));
            return new StrangeFruitConsumedLog(target.Name, AttackBonus, EffectDuration);
        }
    }

    public class HealingPotion : Consumable
    {
        public int HPRestoration = 5;

        public HealingPotion() : base("healing potion") { }

        public override IGameLog Use(Entity target)
        {
            target.RestoreHP(HPRestoration);
            return new HealingPotionConsumedLog(target.Name, HPRestoration);
        }
    }
}
