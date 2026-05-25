using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Items
{
    public abstract class Equipment : Item
    {
        public Stats StatsBonus { get; }

        protected Equipment(string name, Stats bonus) : base(name)
        {
            StatsBonus = bonus;
        }

        public abstract IGameLog Equip(Entity entity);
    }

    public abstract class Weapon : Equipment
    {
        protected Weapon(string name, Stats bonus) : base(name, bonus) {}

        public override IGameLog Equip(Entity entity)
        {
            if (entity is Player player)
            {
                return new WeaponEquippedLog(Name, player.Name, StatsBonus);
            }
            return new EmptyLog();
        }
    }

    public class Sword : Weapon
    {
        public Sword() : base("sword", new Stats { Attack = 5 }) { }
    }

    public class Axe : Weapon
    {
        public Axe() : base("axe", new Stats { Attack = 8 }) { }
    }

    public class Knife : Weapon
    {
        public Knife() : base("knife", new Stats { Attack = 3 }) { }
    }
}
