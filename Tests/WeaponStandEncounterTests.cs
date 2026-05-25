using Xunit;
using src;
using System.Collections.Generic;
using src.Items;
using src.Entities;
using src.Core;
using src.Encounters;

namespace Tests
{
    public class WeaponStandEncounterTests
    {
        // Single weapon stand 
        [Fact]
        public void PickWeapon_ShouldEquipWeaponAndClearStandSlot()
        {
            var player = new Player();
            var context = new GameContext(player);

            var sword = new Sword();
            var weapons = new List<Weapon?>
            {
                sword
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 0);

            Assert.Equal(sword, player.Equipment.EquippedWeapon);
            Assert.Null(encounter.Weapons[0]);
        }

        [Fact]
        public void PickWeapon_ShouldExchangeWeaponWithStandSlot()
        {
            var player = new Player();
            var axe = new Axe();
            var prev = player.Equipment.EquipWeapon(axe);
            var context = new GameContext(player);

            var sword = new Sword();
            var weapons = new List<Weapon?>
            {
                sword
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 0);

            Assert.Null(prev);
            Assert.Equal(sword, player.Equipment.EquippedWeapon);
            Assert.Equal(axe, encounter.Weapons[0]);
        }

        [Fact]
        public void PickWeapon_ShouldPutTheWeaponOutOnTheStandSlot()
        {
            var player = new Player();
            var axe = new Axe();
            var prev = player.Equipment.EquipWeapon(axe);
            var context = new GameContext(player);

            var weapons = new List<Weapon?>
            {
                null
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 0);

            Assert.Equal(axe, weapons[0]);
            Assert.Null(player.Equipment.EquippedWeapon);
        }

        [Fact]
        public void PickWeapon_ShouldLeaveStandSlotAndPlayerWeaponSlotEmpty()
        {
            var player = new Player();
            var context = new GameContext(player);

            var weapons = new List<Weapon?>
            {
                null
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 0);

            Assert.Null(weapons[0]);
            Assert.Null(player.Equipment.EquippedWeapon);
        }

        [Fact]
        public void PickWeapon_ShouldIgnoreInvalidIndex()
        {
            var player = new Player();
            var axe = new Axe();
            var prev = player.Equipment.EquipWeapon(axe);
            var context = new GameContext(player);

            var sword = new Sword();
            var weapons = new List<Weapon?>
            {
                sword
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 10);

            Assert.Equal(axe, player.Equipment.EquippedWeapon);
            Assert.Equal(sword, encounter.Weapons[0]);
        }

        // Multiple weapon stands
        [Fact]
        public void PickWeapon_ShouldChangeOnlyOneStandSlot()
        {
            var player = new Player();
            var axe = new Axe();
            var sword = new Sword();
            var knife = new Knife();
            var prev = player.Equipment.EquipWeapon(axe);
            var context = new GameContext(player);

            var weapons = new List<Weapon?>
            {
                sword,
                knife,
                null
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 1);

            Assert.Equal(sword, weapons[0]);
            Assert.Equal(axe, weapons[1]);
            Assert.Null(weapons[2]);
            Assert.Equal(knife, player.Equipment.EquippedWeapon);
        }

        [Fact]
        public void PickWeapon_SequentiallyChooseDifferentStands()
        {
            var player = new Player();
            var axe = new Axe();
            var sword = new Sword();
            var knife = new Knife();
            var prev = player.Equipment.EquipWeapon(axe);
            var context = new GameContext(player);

            var weapons = new List<Weapon?>
            {
                sword,
                knife
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 0);
            encounter.PickWeapon(context, 1);

            Assert.Equal(axe, weapons[0]);
            Assert.Equal(sword, weapons[1]);
            Assert.Equal(knife, player.Equipment.EquippedWeapon);
        }

        [Fact]
        public void PickWeapon_SequentiallyChooseTheSameStand()
        {
            var player = new Player();
            var axe = new Axe();
            var sword = new Sword();
            var knife = new Knife();
            var context = new GameContext(player);

            var weapons = new List<Weapon?>
            {
                sword,
                knife,
                axe
            };
            var encounter = new WeaponStandEncounter(weapons);

            encounter.PickWeapon(context, 2);
            encounter.PickWeapon(context, 2);
            encounter.PickWeapon(context, 2);

            Assert.Equal(sword, weapons[0]);
            Assert.Equal(knife, weapons[1]);
            Assert.Null(weapons[2]);
            Assert.Equal(axe, player.Equipment.EquippedWeapon);
        }
    }
}