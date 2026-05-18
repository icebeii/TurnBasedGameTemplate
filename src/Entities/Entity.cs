using System;
using System.Collections.Generic;

namespace src.Entities
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public Stats Stats { get; protected set; }
        public bool IsAlive => Stats.CurrentHealth > 0;
    }
}
