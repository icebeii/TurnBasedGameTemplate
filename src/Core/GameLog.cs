using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Core
{
    public interface IGameLog
    {
        string GetMessage();
    }

    public class EnemyDefeatedLog : IGameLog
    {
        private readonly string _enemyName;

        public EnemyDefeatedLog(string enemyName)
        {
            _enemyName = enemyName;
        }

        public string GetMessage()
        {
            return $"{_enemyName} is defeated!";
        }
    }

    public class DamageLog : IGameLog
    {
        private readonly string _source;
        private readonly string _target;
        private readonly int _damage;

        public DamageLog(string source, string target, int damage)
        {
            _source = source;
            _target = target;
            _damage = damage;
        }

        public string GetMessage()
        {
            return $"{_source} deals {_damage} damage to {_target}";
        }
    }

    public class  EffectLog : IGameLog
    {
        private readonly string _target;
        private readonly string _state;
        public EffectLog(string target, string state)
        {
            _target = target;
            _state = state;
        }

        public string GetMessage()
        {
            return $"{_target} {_state}";
        }
    }

    public class  WeaponEquippedLog : IGameLog
    {
        private readonly string _weapon;
        private readonly string _name;
        private readonly Stats _statsBonus;

        public WeaponEquippedLog(string weapon, string name, Stats statsBonus)
        {
            _weapon = weapon;
            _name = name;
            _statsBonus = statsBonus;
        }

        public string GetMessage()
        {
            return $"{_name} picks up the {_weapon}. Attack increased by {_statsBonus.Attack}";
        }
    }

    public class WeaponDroppedLog : IGameLog
    {
        private readonly string _weapon;
        private readonly string _name;

        public WeaponDroppedLog(string weapon, string name)
        {
            _weapon = weapon;
            _name = name;
        }

        public string GetMessage()
        {
            return $"{_name} drops the {_weapon}.";
        }
    }

    public class TrapEscapedLog : IGameLog
    {
        private readonly string _name;
        private int _damage;

        public TrapEscapedLog(string name, int damage)
        {
            _name = name; 
            _damage = damage;
        }

        public string GetMessage()
        {
            return $"{_name} escaped from the trap and took {_damage} damage.";
        }
    }

    public class FountainWaterSipLog : IGameLog
    {
        private readonly string _name;
        private readonly int _hpRestoration;

        public FountainWaterSipLog(string name, int hpRestoration)
        {
            _name = name;
            _hpRestoration = hpRestoration;
        }

        public string GetMessage()
        {
            return $"{_name} takes a sip of water from the fountain. {_hpRestoration} HP restored.";
        }
    }

    public class StrangeFruitConsumedLog : IGameLog
    {
        private readonly string _name;
        private readonly int _attackBonus;
        private readonly int _duration;

        public StrangeFruitConsumedLog(string name, int attackBonus, int duration)
        {
            _name = name;
            _attackBonus = attackBonus;
            _duration = duration;
        }

        public string GetMessage()
        {
            return $"{_name} consumed the strange fruit. Attack increased by {_attackBonus} for {_duration} turns.";
        }
    }

    public class HealingPotionConsumedLog : IGameLog
    {
        private readonly string _name;
        private readonly int _hpRestoration;

        public HealingPotionConsumedLog(string name, int hpRestoration)
        {
            _name = name;
            _hpRestoration= hpRestoration;
        }

        public string GetMessage()
        {
            return $"{_name} consumed the healing potion. {_hpRestoration} HP restored.";
        }
    }

    public class ItemWasTakenLog : IGameLog
    {
        private readonly string _name;
        private readonly string _item;

        public ItemWasTakenLog(string name, string item)
        {
            _name = name;
            _item = item;
        }

        public string GetMessage()
        {
            return $"{_name} took {_item}.";
        }
    }

    public class FailedTakeItemLog : IGameLog
    {
        private readonly string _name;
        private readonly string _item;

        public FailedTakeItemLog(string name, string item)
        {
            _name = name;
            _item = item;
        }

        public string GetMessage()
        {
            return $"{_name} failed to take {_item}.";
        }
    }

    public class EmptyLog : IGameLog
    {
        public string GetMessage()
        {
            return "";
        }
    }
}
