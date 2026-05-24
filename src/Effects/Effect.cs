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
        public int DefenseMultiplier { get; }

        public override string EntityState => "is defending";

        public DefenseBuff()
        {
            Duration = 1;
            DefenseMultiplier = 2;
        }
    }
}
