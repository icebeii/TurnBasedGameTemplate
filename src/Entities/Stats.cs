using System;
using System.Collections.Generic;

namespace src.Entities
{
    /// <summary>
    /// Represents the combat statistics of an entity
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Gets or sets the maximum health value of the entity
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the current health value of the entity
        /// </summary>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// Gets or sets the attack power of the entity
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Gets or sets the defense value of the entity
        /// </summary>
        public int Defense {  get; set; }
    }
}
