using src.Core;
using src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }

    public class Sword : Weapon
    {
        public Sword() : base("sword", new Stats { Attack = 5 }) { }
    }
}
