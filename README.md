# Turn-Based Combat Arena with Random Events

## Introduction

A simple console-based roguelike game written in C#. The player takes control of an adventurer exploring a dangerous dungeon filled with enemies, traps, magical objects, and valuable equipment. The objective is to survive as long as possible while progressing through an endless sequence of randomly generated encounters.

Encounters are generated dynamically, meaning that the player never knows what awaits behind the next door. Some rooms may contain enemies, while others may offer opportunities to recover health, acquire better equipment, or collect useful consumable items.

The game ends when the player's health reaches zero. The final score is determined by the number of completed encounters.

---

## Requirements and Launching the Game

The project requires a recent version of the .NET SDK.

To start the game, open a terminal in the project directory and execute:

```bash
dotnet run
```

After launching, the game immediately creates a new player character and displays the player's initial statistics. The main game loop then begins and continues until the player dies.

All interaction is performed through the console. Whenever the game requests a choice, enter the number corresponding to the desired option and press Enter.

---

## Gameplay Overview

The game is divided into encounters. Each encounter represents a room or situation that the player must resolve before moving forward.

After an encounter is completed, the game generates a new one. This process repeats indefinitely until the player is defeated.

The following encounter types can appear during exploration:

* Combat encounters
* Weapon stand encounters
* Item encounters
* Traps
* Healing fountains
* Shrines

Different encounter types serve different purposes. Some of them are dangerous and reduce the player's health, while others restore it.

---

## Player Progression

The player begins the game at level 1 with a fixed amount of health, attack power, and defense.

Defeating enemies grants experience points. Once enough experience has been accumulated, the player gains a level. Leveling up permanently improves the player's combat statistics and makes future encounters easier to survive.

The experience required for each new level increases progressively. As a result, reaching higher levels becomes increasingly difficult.

When a level is gained, the following bonuses are applied:

* Maximum health increases
* Attack increases
* Defense increases

Enemy strength also scales with the player's level, ensuring that the game remains challenging throughout a run.

---

## Combat System

Combat is the central gameplay mechanic. Whenever a combat encounter occurs, one or more (up to 5) enemies appear. The player must defeat all enemies to proceed.

At the beginning of each turn, the game presents three available actions:

1. Attack
2. Defend
3. Use Item

### Attack

Selecting Attack causes the player to deal damage to an enemy. The amount of damage depends on the player's attack statistic, the enemy's defense statistic, active effects, and the currently equipped weapon.

### Defend

Selecting Defend temporarily increases the player's defensive capability. This reduces the damage received from future enemy attacks.

Defensive effect only lasts for a one enemy turn.

### Use Item

If the inventory contains consumable items, the player may choose one to use during combat. Using an item immediately applies its effect and removes it from the inventory.

If the inventory is empty, the action has no effect.

### Enemy Behaviour

Enemies act after the player's turn. Most enemies attack directly, although damaged enemies may occasionally choose to defend themselves instead.

Combat ends when either:

* All enemies are defeated.
* The player's health reaches zero.

---

## Enemies

The dungeon contains several enemy types.

### Goblin

Goblins are balanced opponents with moderate health, attack power, and defense. They serve as general-purpose enemies and are commonly encountered.

### Spider

Spiders possess relatively low health but compensate with high attack power. They can deal significant damage if not defeated quickly.

### Skeleton

Skeletons are durable enemies with balanced offensive and defensive capabilities. They tend to survive longer than other enemy types.

Enemy statistics scale with the player's current level, making higher-level encounters progressively more dangerous.

---

## Weapons

Throughout the game, the player may encounter weapon stands. These rooms contain one or more weapons that can be equipped. 
Some weapon stands may also be epmty, but but each weapon stand encounter is guaranteed to have at least one weapon.

Equipping a weapon automatically replaces the currently equipped weapon. The previous weapon is returned to the stand.

Each weapon modifies combat in a different way.

### Sword

The sword provides a reliable attack bonus without any additional effects.

### Axe

The axe grants a larger attack bonus and additionally deals a random amount of bonus damage whenever the player attacks.

### Knife

The knife provides a smaller attack bonus but introduces a chance to perform a critical strike, greatly increasing damage dealt.

Choosing the appropriate weapon depends on the player's preferred playstyle and current situation.

---

## Inventory and Consumables

The player possesses an inventory with limited capacity. Consumable items found during exploration can be stored and later used during combat.

If the inventory is already full, newly discovered items cannot be collected.

The game currently includes two consumable items.

### Healing Potion

Healing Potions restore a portion of the player's health immediately after use.

They are useful for recovering from difficult encounters and extending a run.

### Strange Fruit

Strange Fruits temporarily increase attack power for several turns.

They are especially effective when preparing for a difficult combat encounter.

---

## Special Encounters

Not all encounters involve combat.

### Traps

Trap encounters immediately inflict damage on the player. The amount of damage is fixed and cannot be avoided.

### Healing Fountains

Healing fountains restore a portion of the player's health. These encounters provide an opportunity to recover after difficult battles.

### Shrines

Shrines present a choice.

The player may either pray or ignore the shrine. Praying grants a blessing and restores health. Ignoring the shrine results in a curse that damages the player.

This encounter introduces a simple risk-reward decision and breaks the otherwise predictable flow of exploration.

---

## High Scores

When the player dies, the game calculates the final score.

The score corresponds to the total number of completed encounters during the run. If the score exceeds the previous best result, it becomes the new high score.

High score data is stored in the file:

```text
highscore.txt
```

The file is created automatically when necessary and does not require manual editing.

---

## Winning and Losing

The goal of this game is to survive as long as possible and achieve the highest score.

The game ends immediately when the player's health reaches zero. At that point, the final statistics and score are displayed, and the high score is updated if a new record has been achieved.

