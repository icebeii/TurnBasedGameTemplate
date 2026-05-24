using src.Core;
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
}
