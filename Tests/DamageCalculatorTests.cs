using Xunit;
using System;
using System.Collections.Generic;
using src.Entities;
using src.Combat;
using src.Core;
using src.Effects;
using src.Items;

namespace Tests
{
    public class DamageCalculatorTests
    {
        [Fact]
        public void CalculateDamage_ShouldSubtractDefense()
        {
            var attacker = new Player();
            var target = new Goblin(1);

            attacker.Stats.Attack = 10;
            target.Stats.Defense = 3;

            int damage = DamageCalculator.CalculateDamage(attacker, target);
            Assert.Equal(7, damage);
        }

        [Fact] 
        public void CalculateDamage_ShouldDealAtLeastOne()
        {
            var attacker = new Player();
            var target = new Goblin(1);

            attacker.Stats.Attack = 5;
            target.Stats.Defense = 6;

            int damage = DamageCalculator.CalculateDamage(attacker, target);
            Assert.Equal(1, damage);
        }

        [Fact]
        public void CalculateDamage_ShouldIncludeDefenseBuff()
        {
            var attacker = new Player();
            var target = new Goblin(1);

            attacker.Stats.Attack = 10;
            target.Stats.Defense = 3;
            target.Effects.Add(new DefenseBuff(3, 3));

            int damage = DamageCalculator.CalculateDamage(attacker, target);
            Assert.Equal(4, damage);
        }

        [Fact] 
        public void CalculateDamage_ShouldDecreaseDefenseBuffDuration()
        {
            var attacker = new Player();
            var target = new Goblin(1);

            attacker.Stats.Attack = 10;
            target.Stats.Defense = 3;
            target.Effects.Add(new DefenseBuff(3, 3));

            int damage = DamageCalculator.CalculateDamage(attacker, target);
            Assert.Equal(2, target.Effects[0].Duration);
        }

        [Fact]
        public void CalculateDamage_ShouldIncludeWeaponBonus()
        {
            Player attacker = new Player();
            var target = new Goblin(1);

            attacker.Stats.Attack = 10;
            target.Stats.Defense = 3;
            attacker.Equipment.EquipWeapon(new Sword());

            int damage = DamageCalculator.CalculateDamage(attacker, target);
            Assert.Equal(12, damage);
        }

        [Fact]
        public void CalculateDamage_ShouldStackDefenseBuffs()
        {
            var attacker = new Player();
            var target = new Goblin(1);

            attacker.Stats.Attack = 10;
            target.Stats.Defense = 3;
            target.Effects.Add(new DefenseBuff(3, 3));
            target.Effects.Add(new DefenseBuff(1, 3));

            int damage = DamageCalculator.CalculateDamage(attacker, target);
            Assert.Equal(1, damage);
        }
    }
}
