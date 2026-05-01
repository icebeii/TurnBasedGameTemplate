# Specification of the final project for relevant C# courses
(When modifying this document, please maintain the layout and structure and follow the inline instructions.)

## C# Courses selection
(Change `[ ]` to `[x]` for the courses you plan to use this final project for.)

- [x] NPRG035 (Programming in C# language | Programování v jazyce C#)
- [ ] NPRG038 (Advanced C# Programming | Pokročilé programování v jazyce C#)
- [ ] NPRG057 (Advanced .NET Programming II | Pokročilé programování pro .NET II)
- [ ] NPRG064 (Programming user interfaces in .NET | Programování uživatelských rozhraní v .NET)

## Specification

### Turn-Based Combat Arena with Random Events
---
The goal of the project is to develop an extensible framework for turn-based, event-driven games that also functions as a console game.
I want to implement a template with a suitable architecture that can easily accommodate new features, such as new entities, events, weapons, items, etc.

#### Use case scenarios
From a player’s perspective, the application provides an interactive experience in which the user progresses through a sequence of encounters, engages in combat, collects items, and improves their character over time. The player makes decisions through a command-line interface, influencing the outcome of each encounter.

From a developer’s perspective, the application acts as a structured foundation for building similar games. A developer can extend the system by introducing new types of enemies, items, or events without modifying the core game engine. For example, a new enemy type can be implemented by inheriting from a base enemy class, while new item types can be introduced through a shared item abstraction.

#### Main features
The application is structured around a continuous sequence of encounters. Each encounter represents a discrete situation that the player must resolve before progressing further. Encounters are dynamically generated and can be either combat or non-combat events.

Combat encounters represent the core gameplay mechanic. When a combat encounter begins, the player faces an enemy with its own set of attributes. The combat system is turn-based, meaning that the player and the enemy act in alternating turns until one of them is defeated.

During the player’s turn, the user can choose from several actions: attacking the enemy, defending to reduce incoming damage, or using an item from the inventory. After that the enemy performs its action based on a predefined simple decision logic. This may include attacking or defending.

Damage calculation is based on the relationship between attack and defense attributes. Defensive actions temporarily reduce incoming damage.

The player is defined by a set of attributes such as health, attack, and defense. The player can equip a single weapon at a time, which modifies combat behavior. Different weapon types provide distinct playstyles, such as balanced performance, increased damage, or special effects like critical hits.

The game includes an inventory system that allows the player to collect and use items. Items provide various effects, such as restoring health, reducing incoming damage for a limited time, etc. Items are consumed upon use, and the inventory size is limited.

Enemies are generated with varying difficulty levels and attributes.

Between combat encounters, the player may encounter random events. These events represent non-combat situations such as finding items, receiving healing, taking damage, or being presented with a choice that involves risk and reward. Event outcomes may be deterministic or based on probability, increasing replayability.

The difficulty of the game gradually increases as the player progresses through encounters. This is achieved by adjusting enemy strength and the likelihood of more challenging encounters.

The game continues until the player’s health reaches zero. The player’s performance is evaluated based on the number of encounters successfully completed, which serves as the final score.

#### Example program
After starting the program, a welcome message and an action menu is displayed:
```
Welcome to the game!
1. Start new game
2. Exit
```

The player enters 1 to start a new game and the initial statistics are displayed:
```
Game started!

Player stats:
HP: 100/100
Attack: 10
Defense: 5
Level: 1
Experience: 0
```

The first encounter is generated:
```
You encounter an enemy!

Enemy HP: 30
Choose action:
1. Attack
2. Defend
3. Use Item
```

The player attacks the enemy:
```
You deal 10 damage.
Enemy deals 5 damage.

Your HP: 95/100
Enemy HP: 20
```

The combat continues until the enemy is defeated:
```
You deal 10 damage.
Enemy is defeated!

You gained 20 experience.
Your HP: 80/100
Total XP: 20
```

After gaining enough experience, the player levels up:
```
Level up!
New stats:
HP: 110/110
Attack: 12
Defense: 6
```

The player proceeds to the next event:
```
You found an item!

You obtained: Health Potion
```

Later, the player encounters a negative event:
```
Suddenly a storm started!

You take 15 damage.
Current HP: 95/110
```

After a difficult fight, the player is defeated:
```
You were defeated...

Game Over!
Encounters completed: 7
Final Level: 2

New high score!
High score: 7
```

#### Testing
The application will be tested using a combination of manual and automated unit testing.

Manual testing will be conducted to validate the player experience by running multiple game sessions and verifying that all game mechanics function correctly.

Automated unit tests will be implemented to ensure the correctness of key logic components. These tests will focus on important parts of the system, such as damage calculations, leveling mechanics, and so on. Additional components may also be tested where appropriate to ensure overall correctness of the system.