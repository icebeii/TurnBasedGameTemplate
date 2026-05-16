using System;
using System.Collections.Generic;

namespace src.Entities
{
    public abstract class Entity
    {
        public Stats Stats { get; protected set; }
        public bool IsAlive => Stats.CurrentHealth > 0;
    }
}
