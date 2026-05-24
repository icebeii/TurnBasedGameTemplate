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

    public class EmptyLog : IGameLog
    {
        public string GetMessage()
        {
            return "";
        }
    }
}
