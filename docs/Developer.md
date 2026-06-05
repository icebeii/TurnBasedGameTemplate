# Developer Guide

## Introduction

The project was designed with extensibility as one of its primary goals. Most gameplay systems are built around abstractions such as interfaces and base classes, allowing new content to be added with minimal modifications to existing code.

The game consists of a single executable project organized into several logical modules. Each module is responsible for a specific aspect of the game, such as combat, encounters, items, or entities.

A developer extending the game should typically be able to add new content by creating new classes that inherit from existing abstractions.

---

## High-Level Architecture

The application follows a simple game loop managed by the `Engine` class.

The execution flow is:

1. The game creates a `Player` and a `GameContext`.
2. The `EncounterGenerator` creates a random encounter.
3. The generated encounter is executed.
4. The encounter interacts with the player and game state.
5. The process repeats until the player dies.

The `GameContext` acts as a shared state container that is passed between systems. It stores objects that need to be accessible throughout the application, such as the player, random number generator, encounter counter, and game logs.

This architecture avoids the use of global game state and makes individual systems easier to test and extend.

---

## Project Structure

The source code is divided into several namespaces.

### Core

The Core module contains infrastructure and application-level logic.

Important classes:

* `Engine`
* `GameContext`
* `IGameLog`
* `Output`
* `InputHandler`
* `HighScoreManager`

The `Engine` class controls the application's lifecycle and serves as the entry point for gameplay.

The `GameContext` class is shared between systems and represents the current game state.

Input and output are abstracted through the `IOutputHandler` interface. Although the current implementation uses console output, alternative implementations could be introduced without modifying gameplay code.


### Entities

The Entities module contains all living actors that participate in gameplay.

The abstract `Entity` class provides:

* Health management
* Effect management
* Damage handling
* Healing

Two major branches derive from `Entity`:

* `Player`
* `Enemy`

The `Player` class adds progression systems such as experience, leveling, inventory management, and equipment handling.

The `Enemy` class serves as a template for all enemy types. Enemy scaling is implemented through abstract properties defining base statistics and per-level growth values.

Current enemy implementations are:

* `Goblin`
* `Spider`
* `Skeleton`

Adding a new enemy usually requires creating only a single new class.


### Combat

The Combat module contains all combat-related functionality.

Combat is coordinated by `CombatManager`, which executes player turns, enemy turns, and effect processing.

Actions available during combat implement the `ICombatAction` interface.

The core method is:

```csharp
IGameLog PerformAction(Entity actor, Entity? target);
```

This method defines the full lifecycle of a combat action:

* `actor`represents the entity performing the action.
* `target` represents the entity affected by the action. It may be null if the action does not require a target (for example, using an item or defending).
* `The method` returns an IGameLog object describing the result of the action.

Current implementations are:

* `AttackAction`
* `DefendAction`
* `UseItemAction`

Because combat actions share a common interface, new actions can be added without changing the combat system itself.

Damage calculations are delegated to `DamageCalculator`.

Separating damage computation from combat flow keeps combat logic independent from balancing formulas and makes future modifications easier.


### Encounters

The Encounters module contains all dungeon events.

Every encounter implements the `IEncounter` interface.

Current implementations are:

* `CombatEncounter`
* `WeaponStandEncounter`
* `ItemEncounter`
* `TrapEncounter`
* `HealingFountainEncounter`
* `ShrineEncounter`

The `EncounterGenerator` is responsible for encounter selection.

Encounters are generated using a weighted random selection algorithm. Each encounter type is assigned a probability weight that depends partly on the player's level. At runtime, these weights are summed into a total range, and a random number is generated within that range.

The algorithm then iterates through encounter types, subtracting their weights from the random value until the corresponding encounter is selected.


### Items

The Items module contains all collectible and usable objects.

The inheritance hierarchy is simple:

```text
Item
|- Consumable
|- Equipment
    |- Weapon
```

Consumables provide immediate effects when used.

Equipment modifies player capabilities while equipped.

Current consumables include:

* Healing potion
* Strange fruit

Current weapons include:

* Sword
* Axe
* Knife

Weapons may override damage behavior through the `ModifyDamage()` method.

Both `Consumable` and `Equipment` items have the method which should return an `IGameLog` instance, containing information about an consumption/equipment action.

This allows individual weapons to implement unique mechanics without affecting the combat system.


### Effects

The Effects module contains temporary status modifiers.

All effects inherit from the abstract `Effect` class.

In this version all effects are temporary and have a `Duration` property, calculated in turns.

Current effects include:

* `AttackBuff`
* `DefenseBuff`

Effects are attached directly to entities and are processed during combat.

### Logging System

The game uses a dedicated logging system based on the `IGameLog` interface.

Instead of directly printing messages to the console, gameplay systems create log objects that describe events occurring during the game. These objects are then passed to `GameContext.AddLog()`, which stores the log and displays it through the output handler.

Examples of logged events include:

* Damage dealt during combat
* Enemy defeats
* Item consumption
* Weapon changes
* Shrine outcomes
* Trap activations
* Fountain healing

This approach separates gameplay logic from presentation logic. Combat, encounters, and items only describe what happened by creating appropriate log objects, while the output system is responsible for deciding how that information is displayed.

Adding a new log type typically requires creating a new class implementing `IGameLog` and implementing the `GetMessage()` method.

This design makes it possible to replace the current console interface with another user interface in the future without modifying gameplay systems. For example, a graphical interface could display log messages in a dedicated event panel while reusing the same log objects.

### High Score System

The game includes a simple high score system implemented in the `HighScoreManager` class.

Its purpose is to persist the best result between game sessions. The score is based on the number of completed encounters during a run.

The system uses a file-based storage approach. The high score is stored in a plain text file:

```text id="hsfile"
highscore.txt
```

---

## Design Decisions

Several design decisions were made specifically to improve extensibility.

### Interface-Based Encounters

The encounter system uses the `IEncounter` interface.

The game loop does not need to know which encounter is currently being executed. It only calls `Execute()`.


### Interface-Based Combat Actions

Combat actions use the `ICombatAction` interface.

The combat manager interacts with actions through a common abstraction.

This allows additional combat mechanics to be introduced later, such as:

* Magic spells
* Special attacks
* Escape actions
* Summoning abilities

without redesigning combat flow.


### Encapsulated Damage Calculation

Damage calculations are isolated in `DamageCalculator`.

This prevents combat code from becoming tightly coupled to balancing formulas.

Future changes such as:

* elemental damage,
* armor penetration,
* critical hit systems,
* damage-over-time effects

can be implemented primarily inside the calculator.


### Abstract Enemy Definitions

Enemy scaling is centralized in the `Enemy` base class.

Individual enemy classes define only:

* base statistics,
* scaling values,
* base experience reward.

The scaling algorithm itself exists only once.

---

## Extending the Project

One of the primary goals of the project is to make content creation straightforward.

### Adding a New Enemy

Create a new class derived from `Enemy`.

Example steps:

1. Define base statistics.
2. Define scaling statistics.
3. Define the experience reward.
4. Register the enemy inside `EncounterGenerator.GenerateEnemy()`.


### Adding a New Weapon

Create a new class derived from `Weapon`.

The weapon may optionally override `ModifyDamage()` to introduce unique mechanics.

After implementation, register the weapon in `EncounterGenerator.GenerateWeapon()`.


### Adding a New Consumable

Create a new class derived from `Consumable`.

Implement the `Use()` method and return an appropriate log object.

Afterward, add generation logic to the encounter generator.


### Adding a New Effect

Create a new class derived from `Effect`.

The effect may expose any additional data needed by the combat system.

Depending on the mechanic, support for the new effect may need to be added to `DamageCalculator` or other systems that process effects.


### Adding a New Encounter

Create a class implementing `IEncounter`.

Implement the `Execute(GameContext context)` method.

Finally, register the encounter inside `EncounterGenerator`.


### Adding a New Output System

All output operations are performed through the `IOutputHandler` interface.

The current implementation, `ConsoleOutputHandler`, writes information directly to the console. However, gameplay systems never communicate with the console directly and instead use the shared output handler.

To create a new output system:

1. Create a class implementing `IOutputHandler`.
2. Implement all interface methods.
3. Assign the implementation to `Output.Handler` during application startup.

For example, it would be possible to create a GUI output handler, a file-based logger, etc.


## Future Improvements

Several areas could be extended in future versions of the project.

Potential improvements include:

* Additional enemy types
* Additional weapons and consumables
* More complex status effects
* More equipment items and multiple equipment slots
* Save/load functionality
* Advanced enemy AI
* Multiple combat targets

The current architecture was intentionally designed so that these features can be added incrementally without requiring a complete redesign of existing systems.

---
