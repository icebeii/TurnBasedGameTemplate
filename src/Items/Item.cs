using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Items
{
    /// <summary>
    /// Represents a base item
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// Gets the name of the item
        /// </summary>
        public string Name { get; }

        protected Item(string name)
        {
            Name = name;
        }
    }
}
