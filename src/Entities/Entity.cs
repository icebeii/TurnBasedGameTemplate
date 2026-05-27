using src.Effects;
using System;
using System.Collections.Generic;

namespace src.Entities
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public Stats Stats { get; protected set; }

        public List<Effect> Effects { get; }
        public bool IsAlive => Stats.CurrentHealth > 0;
        protected Entity()
        {
            Effects = new List<Effect>();
        }

        public virtual void ApplyEffect(Effect effect)
        {
            Effects.Add(effect);
        }

        public virtual void TakeDamage(int damage)
        {
            Stats.CurrentHealth -= damage;

            if (Stats.CurrentHealth < 0)
            {
                Stats.CurrentHealth = 0;
            }
        }

        public virtual void RestoreHP(int hpRestoration)
        {
            Stats.CurrentHealth += hpRestoration;

            if (Stats.CurrentHealth > Stats.MaxHealth)
            {
                Stats.CurrentHealth = Stats.MaxHealth;
            }
        }

        public virtual void PrintCurrentHP()
        {
            Console.WriteLine($"{Name} " + "HP: " + $"{Stats.CurrentHealth}" + "/" + $"{Stats.MaxHealth}");
        }
    }
}
