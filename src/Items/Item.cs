using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Items
{
    public abstract class Item
    {
        public string Name { get; }
        protected Item(string name)
        {
            Name = name;
        }
    }
}
