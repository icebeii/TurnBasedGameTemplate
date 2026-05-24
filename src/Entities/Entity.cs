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
            
    }
}
