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

        public virtual int ModifyDamage(int damage)
        {
            return damage;
        }
    }

    public class Sword : Weapon
    {
        public Sword() : base("Sword", new Stats { Attack = 5 }) { }
    }

    public class Axe : Weapon
    {
        public Axe() : base("Axe", new Stats { Attack = 8 }) { }
        public override int ModifyDamage(int damage)
        {
            Random random = new Random();
            int bonus = random.Next(3, 8);
            return bonus + damage;
        }
    }

    public class Knife : Weapon
    {
        private const int CritChance = 30;
        private const double CritMultiplier = 2;

        public Knife() : base("Knife", new Stats { Attack = 3 }) { }
        public override int ModifyDamage(int damage)
        {
            Random random = new Random();
            int roll = random.Next(100);
            if (roll < CritChance)
            {
                return (int)(damage * CritMultiplier);
            }
            return damage;
        }
    }
}
