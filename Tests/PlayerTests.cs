using Xunit;
using src.Entities;
using System;
using System.Collections.Generic;
using src.Core;

namespace Tests
{
    public class PlayerTests
    {
        [Fact]
        public void GainXP_ShouldIncreaseXP()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            player.GainXP(50);
            Assert.Equal(50, player.Experience);
        }
        
        [Fact]
        public void GainXP_ShouldIncreaseLevel()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            player.GainXP(100);
            Assert.Equal(2, player.Level);
        }

        [Fact]
        public void GainXP_ShouldHandleXPOverflow()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            player.GainXP(150);
            Assert.Equal(50, player.Experience);
            Assert.Equal(2, player.Level);
        }

        [Fact]
        public void GainXP_ShouldHandleMultipleLevelUps()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            player.GainXP(500);
            Assert.True(player.Level >= 3);
        }

        [Fact]
        public void GainXP_ShouldProcessMultipleCalls()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            player.GainXP(50);
            player.GainXP(10);
            player.GainXP(20);
            Assert.Equal(80, player.Experience);
        }

        [Fact]
        public void GainXP_ShouldNotLevelUpWhenNotEnoughXP()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            player.GainXP(50);
            Assert.Equal(1, player.Level);
        }

        [Fact]
        public void GainXP_ShouldIncreaseStats()
        {
            Output.Handler = new ConsoleOutputHandler();
            var player = new Player();
            int oldAttack = player.Stats.Attack;
            int oldDefense = player.Stats.Defense;
            int oldHP = player.Stats.MaxHealth;

            player.GainXP(100);

            int newAttack = player.Stats.Attack;
            int newDefense = player.Stats.Defense;
            int newHP = player.Stats.MaxHealth;

            Assert.True(newAttack > oldAttack);
            Assert.True(newDefense > oldDefense);
            Assert.True(newHP > oldHP);
        }
    }
}
