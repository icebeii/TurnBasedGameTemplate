using System;
using System.Collections.Generic;

namespace src.Effects
{
    public abstract class Effect
    {
        public int Duration { get; protected set; }
        public abstract string EntityState { get; }

        public void DecreaseDuration()
        {
            Duration--;
        }

        public bool IsExpired() => Duration <= 0;
    }

    public class DefenseBuff : Effect
    {
        public int DefenseBonus { get; }

        public override string EntityState => "is defending";

        public DefenseBuff(int duration, int defenseBonus)
        {
            Duration = duration;
            DefenseBonus = defenseBonus;
        }
    }

    public class AttackBuff : Effect
    {
        public int AttackBonus { get; }

        public override string EntityState => "is full of strength";

        public AttackBuff(int attackBonus, int duration)
        {
            AttackBonus = attackBonus;
            Duration = duration;
        }
    }
}
