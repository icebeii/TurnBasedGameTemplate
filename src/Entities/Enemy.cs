using System;
using System.Collections.Generic;

namespace src.Entities
{
    public class Enemy : Entity
    {
        public string Name { get; }
        public Enemy(string name, int maxHealth, int attack, int defense)
        {
            Name = name;
            Stats = new Stats
            {
                MaxHealth = maxHealth,
                CurrentHealth = maxHealth,
                Attack = attack,
                Defense = defense
            };
        }
    }

    public class Goblin : Enemy
    {
        public Goblin() : base("Goblin", 30, 8, 2) { }
    }
}
