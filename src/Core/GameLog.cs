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

    public class EmptyLog : IGameLog
    {
        public string GetMessage()
        {
            return "";
        }
    }
}
