using src.Encounters;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Core
{
    public class Engine
    {
        private readonly GameContext _context;
        private readonly EncounterGenerator _encounterGenerator;
        public Engine()
        {
            Player player = new Player();
            _context = new GameContext(player);
            _encounterGenerator = new EncounterGenerator();
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Game started!");
            PrintPlayerStats();

            while (_context.Player.IsAlive)
            {
                Console.WriteLine();
                Console.WriteLine("Encounter #" + $"{_context.EncounterCount + 1}");

                IEncounter encounter = _encounterGenerator.Generate(_context);
                encounter.Execute(_context);
                _context.EncounterCount++;

                break; // temporary
            }

            Console.WriteLine();
            Console.WriteLine("Game over!");
            Console.WriteLine("Encounters completed: " + $"{_context.EncounterCount}");
            Console.WriteLine("Final level: " + $"{_context.Player.Level}");
        }

        private void PrintPlayerStats()
        {
            Console.WriteLine();
            Console.WriteLine("Player stats:");
            Console.WriteLine("HP: " + $"{_context.Player.Stats.CurrentHealth}" + "/" + $"{_context.Player.Stats.MaxHealth}");
            Console.WriteLine("Attack: " + $"{_context.Player.Stats.Attack}");
            Console.WriteLine("Defense: " + $"{_context.Player.Stats.Defense}");
            Console.WriteLine("Level: " + $"{_context.Player.Level}");
            Console.WriteLine("XP: " + $"{_context.Player.Experience}");
        }
            
    }
}
